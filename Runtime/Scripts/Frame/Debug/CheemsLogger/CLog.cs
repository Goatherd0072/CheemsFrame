using Cheems.Debug;

namespace Cheems
{
    public static class CLog
    {
        static LogSetting _setting;

        // static Log()
        // {
        //     Init();
        // }

        public static void Init(LogSetting setting)
        {
            _setting = setting;
            LogCtrl.Init(ELoggerType.Unity, _setting.baseConfig, _setting.fileConfig);
        }

        public static void Debug(object msg)
        {
#if ENABLE_LOG_DEBUG
            Debug(msg.ToString());
#endif
        }

        public static void Debug(string msg)
        {
#if ENABLE_LOG_DEBUG
            LogCtrl.Debug(msg);
#endif
        }

        public static void Info(object msg)
        {
#if ENABLE_LOG_INFO
            Info(msg.ToString());
#endif
        }

        public static void Info(string msg)
        {
#if ENABLE_LOG_INFO
            LogCtrl.Info(msg);
#endif
        }

        public static void Warning(object msg)
        {
#if ENABLE_LOG_INFO
            LogCtrl.Warning(msg.ToString());
#endif
        }

        public static void Warning(string msg)
        {
#if ENABLE_LOG_WARNING
            LogCtrl.Warning(msg);
#endif
        }

        public static void Error(object msg)
        {
#if ENABLE_LOG_INFO
            LogCtrl.Error(msg.ToString());
#endif
        }

        public static void Error(string msg)
        {
#if ENABLE_LOG_ERROR
            LogCtrl.Error(msg);
#endif
        }

        public static void Dispose()
        {
#if ENABLE_LOG_DEBUG || ENABLE_LOG_INFO || ENABLE_LOG_WARNING || ENABLE_LOG_ERROR
            LogCtrl.Close();
#endif
        }
    }
}