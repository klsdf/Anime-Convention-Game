using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ACG
{
    public class SimpleWorkQueue
    {
        const string TAG = "SimpleWorkQueue";
        private ConcurrentQueue<Action> _queue;
        private List<Thread> _threadList = new List<Thread>();
        private AutoResetEvent _condition;
        private volatile bool _dispose;
        public SimpleWorkQueue(int threadNum = 1)
        {
            _dispose = false;
            _queue = new ConcurrentQueue<Action>();
            _condition = new AutoResetEvent(false);
            CreateThreadPool(threadNum);
        }

        public void Dispose()
        {
            _dispose = true;
            _condition.Set();
        }

        public void QueueWorkItem(Action action)
        {
            if (_dispose) return;

            _queue.Enqueue(action);
            _condition.Set();
        }

        private void CreateThreadPool(int threadNum)
        {
            for (int i = 0; i < threadNum; i++)
            {
                var t = new Thread(() => RunLoop());
                _threadList.Add(t);
            }

            _threadList.ForEach(t => t.Start());
        }

        private void RunLoop()
        {
            while (true)
            {
                _condition.WaitOne();

                while (_queue.TryDequeue(out var t))
                {
                    try
                    {
                        t?.Invoke();
                    } catch(Exception ex)
                    {
                        LogUtil.e(TAG, ex.ToString());
                    }

                    
                }

                if (_dispose) return;
            }
        }
    }
}