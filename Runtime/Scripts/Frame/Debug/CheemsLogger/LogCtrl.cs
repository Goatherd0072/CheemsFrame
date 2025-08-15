//
// From: 
// JokerFramework
// https://blog.csdn.net/linxinfa/article/details/119280053
//

using System.IO;
using System.Text;
using System.Threading;
using System;
using System.Diagnostics;
using UnityEngine;

namespace Cheems.Debug
{
    public static class LogCtrl
    {
        private static          bool          _isUseLogger = false;
        private static          ILogger       _logger;
        private static          StreamWriter  _logStreamWriter;
        private static          LogBaseConfig _baseConfig;
        private static          LogFileConfig _fileConfig;
        private static readonly LogCleaner    _logCleaner = new();

        public static void Init(ELoggerType loggerType, LogBaseConfig baseConfig, LogFileConfig logFileConfig)
        {
            Close();
            switch (loggerType)
            {
                case ELoggerType.Unity:
                    _logger = new UnityLogger(baseConfig.LogColors);
                    break;
            }

            _baseConfig = baseConfig;
            _fileConfig = logFileConfig;

            var logFile = _fileConfig.SavePath;
            
            if (_fileConfig.NeedSave)
            {
                if (!File.Exists(logFile))
                {
                    _logStreamWriter = File.CreateText(logFile);
                }
                else
                {
                    _logStreamWriter = File.AppendText(logFile);
                }

                _logStreamWriter.AutoFlush = true;
            }

            _isUseLogger = true;
            
            _logCleaner.StartCleaner(Path.GetDirectoryName(logFile));
            // PlayerPrefs.SetString("LastRunTime", DateTime.DaysInMonth(2023,10).ToString(CultureInfo.CurrentCulture));
            // PlayerPrefs.Save();
        }

        private static string DecorateLog(ELogType logType, string msg)
        {
            StringBuilder stringBuilder = new StringBuilder(100);
            stringBuilder.AppendFormat("[{0}] ", logType.ToString());

            WriteTime(stringBuilder);
            WriteThreadID(stringBuilder);

            stringBuilder.AppendFormat(" {0}", msg);

            WriteTrace(stringBuilder);

            string text = stringBuilder.ToString();

            WriteFile(text, logType);


            // Upload();
            return text;
        }

        private static void WriteTime(StringBuilder stringBuilder)
        {
            if (_baseConfig.WriteTime)
            {
                stringBuilder.AppendFormat("{0}", DateTime.Now.ToString(_baseConfig.TimeFormat));
            }
        }

        private static void WriteThreadID(StringBuilder stringBuilder)
        {
            if (_baseConfig.WriteThreadID)
            {
                stringBuilder.AppendFormat(GetThreadID());
            }
        }

        private static void WriteTrace(StringBuilder stringBuilder)
        {
            if (_baseConfig.WriteTrace)
            {
                stringBuilder.AppendFormat(GetTrace());
            }
        }

        private static void WriteFile(string text, ELogType logType)
        {
            if (_fileConfig.NeedSave && ((_fileConfig.SaveLogTypes & logType) != 0))
            {
                WriteToFile(text);
            }
        }

        private static void Upload()
        {
            /// // TODO: 上传
        }

        private static string GetThreadID()
        {
            return $" Thread:{Thread.CurrentThread.ManagedThreadId} ";
        }

        private static string GetTrace()
        {
            StackTrace stackTrace = new StackTrace(_baseConfig.SkipTraceFrameCount, true);
            string text = "\n";
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                StackFrame frame = stackTrace.GetFrame(i);
                text += $"\n  {frame.GetFileName()}:{frame.GetMethod()} line:{frame.GetFileLineNumber()}";
            }

            text += "\n";
            // UnityEngine.Debug.Log(text);
            return text;
        }

        public static void Debug(string msg)
        {
            if (!_isUseLogger)
            {
                UnityEngine.Debug.LogError("未使用封装的日志系统\n" + msg);
                return;
            }

            _logger.Debug(DecorateLog(ELogType.Debug, msg));
        }

        public static void Info(string msg)
        {
            if (!_isUseLogger)
            {
                UnityEngine.Debug.LogError("未使用封装的日志系统\n" + msg);
                return;
            }

            _logger.Info(DecorateLog(ELogType.Info, msg));
        }

        public static void Warning(string msg)
        {
            if (!_isUseLogger)
            {
                UnityEngine.Debug.LogError("未使用封装的日志系统\n" + msg);
                return;
            }

            _logger.Warning(DecorateLog(ELogType.Warning, msg));
        }

        public static void Error(string msg)
        {
            if (!_isUseLogger)
            {
                UnityEngine.Debug.LogError("未使用封装的日志系统\n" + msg);
                return;
            }

            _logger.Error(DecorateLog(ELogType.Error, msg));
        }

        private static void WriteToFile(string text)
        {
            _logStreamWriter?.WriteLine(text, Encoding.UTF8);
        }

        public static void Close()
        {
            _logger = null;
            _logStreamWriter?.Dispose();
            _isUseLogger = false;
        }

        #region 解决日志双击溯源问题

#if UNITY_EDITOR
        [UnityEditor.Callbacks.OnOpenAssetAttribute(0)]
        static bool OnOpenAsset(int instanceID, int line)
        {
            string stackTrace = GetStackTrace();
            if (!string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("Cheems.Debug"))
            {
                // 使用正则表达式匹配at的哪个脚本的哪一行
                var matches = System.Text.RegularExpressions.Regex.Match(stackTrace, @"\(at (.+)\)",
                                                                         System.Text.RegularExpressions.RegexOptions
                                                                               .IgnoreCase);
                string pathLine = "";
                while (matches.Success)
                {
                    pathLine = matches.Groups[1].Value;

                    if (!pathLine.Contains("CheemsLogger"))
                    {
                        int splitIndex = pathLine.LastIndexOf(":");
                        // 脚本路径
                        string path = pathLine.Substring(0, splitIndex);
                        // 行号
                        line = System.Convert.ToInt32(pathLine.Substring(splitIndex + 1));
                        string fullPath =
                            Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
                        fullPath = fullPath + path;
                        // 跳转到目标代码的特定行
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(
                            fullPath.Replace('/', '\\'), line);
                        break;
                    }

                    matches = matches.NextMatch();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取当前日志窗口选中的日志的堆栈信息
        /// </summary>
        /// <returns></returns>
        static string GetStackTrace()
        {
            // 通过反射获取ConsoleWindow类
            var ConsoleWindowType = typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            // 获取窗口实例
            var fieldInfo = ConsoleWindowType.GetField("ms_ConsoleWindow",
                                                       System.Reflection.BindingFlags.Static |
                                                       System.Reflection.BindingFlags.NonPublic);
            var consoleInstance = fieldInfo.GetValue(null);
            if (consoleInstance != null)
            {
                if ((object)UnityEditor.EditorWindow.focusedWindow == consoleInstance)
                {
                    // 获取m_ActiveText成员
                    fieldInfo = ConsoleWindowType.GetField("m_ActiveText",
                                                           System.Reflection.BindingFlags.Instance |
                                                           System.Reflection.BindingFlags.NonPublic);
                    // 获取m_ActiveText的值
                    string activeText = fieldInfo.GetValue(consoleInstance).ToString();
                    return activeText;
                }
            }

            return null;
        }
#endif

        #endregion
    }
}