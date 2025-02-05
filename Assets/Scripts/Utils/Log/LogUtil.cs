using UnityEngine;

namespace ACG{

    public class LogUtil
    {
        /// <summary>
        /// For catching Logcat messages
        /// </summary>
        
        public static int logLev = 1;
        const string TAG = "LogUtil";

        public enum LogLevel: int
        {
            Debug,
            Info,
            Warning,
            Error,
        }

        public static void SetStackTrace(bool enable)
        {
            i(TAG, $"SetStackTrace : {enable}");
            var logType = enable ? StackTraceLogType.ScriptOnly : StackTraceLogType.None;
            Application.SetStackTraceLogType(LogType.Log, logType);
        }

        public static void i(string tag, string msg)
        {
            Log(LogLevel.Info, tag, msg);
        }

        public static void d(string tag, string msg)
        {
            Log(LogLevel.Debug, tag, msg);
        }

        public static void w(string tag, string msg)
        {
            Log(LogLevel.Warning, tag, msg);
        }

        public static void e(string tag, string msg)
        {
            Log(LogLevel.Error, tag, msg);
        }


        private static void Log(LogLevel level, string tag, string msg)
        {
            if((int)level < logLev)
            {
                const string prefix = "ACG";

                if(level ==LogLevel.Error)
                {
                   Debug.LogError($"{prefix}, {tag} : {msg}");
                }   
                else if(level == LogLevel.Warning)
                {
                    Debug.LogWarning($"{prefix}, {tag} : {msg}");
                }
                else
                {
                    Debug.Log($"{prefix}, {tag} : {msg}");
                }
            }
        }


    }
}