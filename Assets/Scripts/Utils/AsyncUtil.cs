using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.Assertions;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ACG
{
    public class SwitchToMainThread : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get => false;
        }
    }

    public class SwitchToThreadPool
    {
        public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
        {
            return Task.Run(() => { }).ConfigureAwait(false).GetAwaiter();
        }
    }

    public class DelayFrames : CustomYieldInstruction
    {
        private int _frameCount;
        private int _startFrame;
       
        public DelayFrames(int frameCount)
        {
            _frameCount = frameCount;
            _startFrame = Time.frameCount;
        }

        public override bool keepWaiting
        {
            get => Time.frameCount< _startFrame + _frameCount;
        }
    }

    public static class SyncContextUtil
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Install()
        {
           UnitySynchronizationContext = SynchronizationContext.Current;
              UnityThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public static int UnityThreadId
        {
            get;
            private set;
        }

        public static SynchronizationContext UnitySynchronizationContext
        {
            get;
            private set;
        }

    }

    public class AsyncCoroutineRunner: MonoBehaviour
    {
        private static AsyncCoroutineRunner _instance;
        public static AsyncCoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("AsyncCoroutineRunner").AddComponent<AsyncCoroutineRunner>();
                }
                return _instance;
            }
        }

        void Awake()
        {
            gameObject.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static class IEnumeratorAwaitExtensions
    {

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitForSeconds instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this SwitchToMainThread instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this DelayFrames instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitForEndOfFrame instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitForFixedUpdate instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitForSecondsRealtime instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitUntil instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter GetAwaiter(this WaitWhile instruction)
        {
            return GetAwaiterReturnVoid(instruction);
        }

        public static SimpleCoroutineAwaiter<AsyncOperation> GetAwaiter(this AsyncOperation instruction)
        {
            return GetAwaiterReturnSelf(instruction);
        }

        public static SimpleCoroutineAwaiter<UnityEngine.Object> GetAwaiter(this ResourceRequest instruction)
        {
            var awaiter = new SimpleCoroutineAwaiter<UnityEngine.Object>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.ResourceRequest(awaiter, instruction)));
            return awaiter;
        }

        public static SimpleCoroutineAwaiter<WWW> GetAwaiter(this WWW instruction)
        {
            return GetAwaiterReturnSelf(instruction);
        }

        public static SimpleCoroutineAwaiter<AssetBundle> GetAwaiter(this AssetBundleCreateRequest instruction)
        {
            var awaiter = new SimpleCoroutineAwaiter<AssetBundle>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.AssetBundleCreateRequest(awaiter, instruction)));
            return awaiter;
        }

        public static SimpleCoroutineAwaiter<UnityEngine.Object> GetAwaiter(this AssetBundleRequest instruction)
        {
            var awaiter = new SimpleCoroutineAwaiter<UnityEngine.Object>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.AssetBundleRequest(awaiter, instruction)));
            return awaiter;
        }


        public static SimpleCoroutineAwaiter<T> GetAwaiter<T>(this IEnumerator<T> coroutine)
        {
                var awaiter = new SimpleCoroutineAwaiter<T>();
                RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                    new CoroutineWrapper<T>(coroutine, awaiter).Run()));
                return awaiter;
         }

        public static SimpleCoroutineAwaiter<object> GetAwaiter(this IEnumerator coroutine)
        {
            var awaiter = new SimpleCoroutineAwaiter<object>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                new CoroutineWrapper<object>(coroutine, awaiter).Run()));
            return awaiter;
        }

        static SimpleCoroutineAwaiter GetAwaiterReturnVoid(object instruction)
        {
            var awaiter = new SimpleCoroutineAwaiter();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.ReturnVoid(awaiter, instruction)));
            return awaiter;
        }

        static SimpleCoroutineAwaiter<T> GetAwaiterReturnSelf<T>(T instruction)
        {
            var awaiter = new SimpleCoroutineAwaiter<T>();
            RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
                InstructionWrappers.ReturnSelf(awaiter, instruction)));
            return awaiter;
        }
        static void RunOnUnityScheduler(Action action)
        {
            if (SynchronizationContext.Current == SyncContextUtil.UnitySynchronizationContext)
            {
                action();
            }
            else
            {
                SyncContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
            }
        }

        public class SimpleCoroutineAwaiter<T> : INotifyCompletion
        {
            bool _isDone;
            Exception _exception;
            Action _continuation;
            T _result;

            public bool IsCompleted => _isDone;

            public T GetResult()
            {
            
                Assert.IsTrue(_isDone," SimpleCoroutineAwaiter.GetResult() called when not done");

                if (_exception != null)
                {
                    throw _exception;
                }
                return _result;
            }

            public void Complete(T result, Exception e)
            {
                Assert.IsFalse(_isDone,"SimpleCoroutineAwaiter.Complete() called when already done");

                _isDone = true;
                _exception = e;
                _result = result;
                if(_continuation != null)
                {
                    RunOnUnityScheduler(_continuation);
                }
            }

            public void OnCompleted(Action continuation)
            {
                Assert.IsNull(_continuation,"OnCompleted called multiple times");
                Assert.IsFalse(_isDone,"OnCompleted called when already done");
                _continuation = continuation;
            }
        }

         public class SimpleCoroutineAwaiter : INotifyCompletion
        {
            bool _isDone;
            Exception _exception;
            Action _continuation;

            public bool IsCompleted => _isDone;

            public void GetResult()
            {
            
                Assert.IsTrue(_isDone," SimpleCoroutineAwaiter.GetResult() called when not done");

                if (_exception != null)
                {
                    throw _exception;
                }
            }

            public void Complete(Exception e)
            {
                Assert.IsFalse(_isDone,"SimpleCoroutineAwaiter.Complete() called when already done");

                _isDone = true;
                _exception = e;

                if(_continuation != null)
                {
                    RunOnUnityScheduler(_continuation);
                }
            }

            public void OnCompleted(Action continuation)
            {
                Assert.IsNull(_continuation,"OnCompleted called multiple times");
                Assert.IsFalse(_isDone,"OnCompleted called when already done");
                _continuation = continuation;
            }
        }


        class CoroutineWrapper<T> 
        {
            readonly SimpleCoroutineAwaiter<T> _awaiter;
            readonly Stack<IEnumerator> _processStack;

            public CoroutineWrapper(IEnumerator enumerator, SimpleCoroutineAwaiter<T> awaiter)
            {
                _processStack = new Stack<IEnumerator>();
                _processStack.Push(enumerator);
                _awaiter = awaiter;
            }

           public IEnumerator Run()
            {
                while (true)
                {
                    var topWorker = _processStack.Peek();

                    bool isDone;

                    try
                    {
                        isDone = !topWorker.MoveNext();
                    }
                    catch (Exception e)
                    {
                        var objectTrace = GenerateObjectTrace(_processStack);

                        if (objectTrace.Any())
                        {
                            _awaiter.Complete(
                                default(T), new Exception(
                                    GenerateObjectTraceMessage(objectTrace), e));
                        }
                        else
                        {
                            _awaiter.Complete(default(T), e);
                        }

                        yield break;
                    }

                    if (isDone)
                    {
                        _processStack.Pop();

                        if (_processStack.Count == 0)
                        {
                            _awaiter.Complete((T)topWorker.Current, null);
                            yield break;
                        }
                    }

                    if (topWorker.Current is IEnumerator)
                    {
                        _processStack.Push((IEnumerator)topWorker.Current);
                    }
                    else
                    {
                        yield return topWorker.Current;
                    }
                }
            }

            string GenerateObjectTraceMessage(List<Type> objTrace)
            {
                var result = new StringBuilder();

                foreach (var objType in objTrace)
                {
                    if (result.Length != 0)
                    {
                        result.Append(" -> ");
                    }

                    result.Append(objType.ToString());
                }

                result.AppendLine();
                return "Unity Coroutine Object Trace: " + result.ToString();
            }

            static List<Type> GenerateObjectTrace(IEnumerable<IEnumerator> enumerators)
            {
                var objTrace = new List<Type>();

                foreach (var enumerator in enumerators)
                {
                    var field = enumerator.GetType().GetField("$this", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                    if (field == null)
                    {
                        continue;
                    }

                    var obj = field.GetValue(enumerator);

                    if (obj == null)
                    {
                        continue;
                    }

                    var objType = obj.GetType();

                    if (!objTrace.Any() || objType != objTrace.Last())
                    {
                        objTrace.Add(objType);
                    }
                }

                objTrace.Reverse();
                return objTrace;
            }
        }

        static class InstructionWrappers
        {
            public static IEnumerator ReturnVoid(
                SimpleCoroutineAwaiter awaiter, object instruction)
            {
                yield return instruction;
                awaiter.Complete(null);
            }

            public static IEnumerator AssetBundleCreateRequest(
                SimpleCoroutineAwaiter<AssetBundle> awaiter, AssetBundleCreateRequest instruction)
            {
                yield return instruction;
                awaiter.Complete(instruction.assetBundle, null);
            }

            public static IEnumerator ReturnSelf<T>(
                SimpleCoroutineAwaiter<T> awaiter, T instruction)
            {
                yield return instruction;
                awaiter.Complete(instruction, null);
            }

            public static IEnumerator AssetBundleRequest(
                SimpleCoroutineAwaiter<UnityEngine.Object> awaiter, AssetBundleRequest instruction)
            {
                yield return instruction;
                awaiter.Complete(instruction.asset, null);
            }

            public static IEnumerator ResourceRequest(
                SimpleCoroutineAwaiter<UnityEngine.Object> awaiter, ResourceRequest instruction)
            {
                yield return instruction;
                awaiter.Complete(instruction.asset, null);
            }
        }

    }

}