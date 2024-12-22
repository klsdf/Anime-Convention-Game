using UnityEngine;
using System;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;



namespace ACG
{
    public static class Util 
    {
        public static async void RunOnMainThread(System.Action action)
        {
            await new SwitchToMainThread();
            action?.Invoke();
        }

        public static async void RunOnWorkThread(System.Action action)
        {
            await new SwitchToThreadPool();
            action?.Invoke();
        }
        public static async Task DelayFrames(int n)
        {
            await new DelayFrames(n);
        }

        public static async Task WaitUntil(Func<bool> action) 
        {
            await new WaitUntil(action);
        }

        public static async Task WaitForSeconds(float sec)
        {
            await new WaitForSeconds(sec);
        }

        public static async Task WaitForEndOfFrame()
        {
            await new WaitForEndOfFrame();
        }

        public static async Task WaitForFixedUpdate()
        {
            await new WaitForFixedUpdate();
        }

        public static string RunCommand(string executable, string arguments, string workingPath)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = executable,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingPath
                };

                var output = new StringBuilder();
                using (var process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.OutputDataReceived += (sender, args) => { output.AppendLine(args.Data); };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        return null;
                    }
                }

                return output.ToString().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Parse an unmanaged value from a byte buffer
        /// </summary>
        /// <param name="byteBuffer">The source byte buffer</param>
        /// <param name="offset">Specify the offset byte index for parsing</param>
        /// <typeparam name="T">Unmanaged value type</typeparam>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"> Throws when there are not enough bytes to parse a value of type T</exception>
        public static unsafe T ParseFromByteBuffer<T>(ref NativeArray<byte> byteBuffer, int offset) where T : unmanaged
        {
            if (byteBuffer == null || byteBuffer.Length < offset + sizeof(T))
                throw new System.IndexOutOfRangeException(
                    $"Array does not contain enough bytes to parse a {nameof(T)} from the specified offset.");

            var bufPtr = (byte*)byteBuffer.GetUnsafePtr() + offset;
            var result = *(T*)bufPtr;
            return result;
        }

        /// <summary>
        /// Create a NativeSlice of type T from a specific range of byte buffer
        /// </summary>
        /// <param name="byteBuffer">The source byte buffer</param>
        /// <param name="startIdx">The start index</param>
        /// <param name="count">The count of T</param>
        /// <typeparam name="T">Unmanaged value type</typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static unsafe NativeSlice<T> SliceByteBuffer<T>(ref NativeArray<byte> byteBuffer, int startIdx, int count) where T : unmanaged
        {
            if (byteBuffer == null || (byteBuffer.Length - startIdx) < count * sizeof(T)
                                   || (byteBuffer.Length - startIdx) % sizeof(T) != 0)
                throw new InvalidOperationException("Invalid buffer.");

            var ret = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(
                (T*)((byte*)byteBuffer.GetUnsafePtr() + startIdx), sizeof(T), count
            );
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref ret,
                NativeArrayUnsafeUtility.GetAtomicSafetyHandle(byteBuffer));
#endif
            return ret;
        }

        /// <summary>
        /// Helper Malloc function
        /// </summary>
        /// <param name="count"></param>
        /// <param name="allocator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static unsafe T* Malloc<T>(uint count, Allocator allocator = Allocator.TempJob) where T : unmanaged
        {
            return (T*)UnsafeUtility.Malloc(
                UnsafeUtility.SizeOf<T>() * count,
                UnsafeUtility.AlignOf<T>(),
                allocator);
        }

    }
}


