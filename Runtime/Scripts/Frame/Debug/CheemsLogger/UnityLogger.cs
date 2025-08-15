using System;
using System.Reflection;
using System.Text;

namespace Cheems.Debug
{
    public class UnityLogger : ILogger
    {
        private MethodInfo _debugFunction;

        private MethodInfo _errorFunction;

        private MethodInfo _warningFunction;

        private readonly string[] _stringColors;

        public UnityLogger(string[] colors)
        {
            Type type = Type.GetType("UnityEngine.Debug,UnityEngine");
            if (type != null)
            {
                _debugFunction = type.GetMethod("Log", new Type[1] { typeof(object) });
                _warningFunction = type.GetMethod("LogWarning", new Type[1] { typeof(object) });
                _errorFunction = type.GetMethod("LogError", new Type[1] { typeof(object) });
            }

            _stringColors = colors;
        }

        public void Debug(string msg)
        {
            msg = Decorate(ELogType.Debug, msg);
            _debugFunction.Invoke(null, new object[1] { msg });
        }

        public void Info(string msg)
        {
            msg = Decorate(ELogType.Info, msg);
            _debugFunction.Invoke(null, new object[1] { msg });
        }

        public void Error(string msg)
        {
            msg = Decorate(ELogType.Error, msg);
            _errorFunction.Invoke(null, new object[1] { msg });
        }

        public void Warning(string msg)
        {
            msg = Decorate(ELogType.Warning, msg);
            _warningFunction.Invoke(null, new object[1] { msg });
        }

        private string Decorate(ELogType ELogType, string msg)
        {
            string[] array = msg.Split(new string[1] { "\n" }, StringSplitOptions.None);
            StringBuilder stringBuilder = new();
            switch (ELogType)
            {
                case ELogType.Info:
                {
                    string[] array2 = array;
                    foreach (string arg in array2)
                    {
                        stringBuilder.AppendFormat("<color={0}>{1}</color>\n", _stringColors[0], arg);
                    }

                    break;
                }
                case ELogType.Warning:
                {
                    string[] array2 = array;
                    foreach (string arg2 in array2)
                    {
                        stringBuilder.AppendFormat("<color={0}>{1}</color>\n", _stringColors[1], arg2);
                    }

                    break;
                }
                case ELogType.Error:
                {
                    string[] array2 = array;
                    foreach (string arg3 in array2)
                    {
                        stringBuilder.AppendFormat("<color={0}>{1}</color>\n", _stringColors[2], arg3);
                    }

                    break;
                }
                case ELogType.Debug:
                {
                    string[] array2 = array;
                    foreach (string arg4 in array2)
                    {
                        stringBuilder.AppendFormat("{0}\n", arg4);
                    }

                    break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}