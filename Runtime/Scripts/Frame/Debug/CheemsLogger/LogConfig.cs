using System;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cheems.Debug
{
    internal enum FilePathType
    {
        PersistentDataPath,
        DataPath,
        StreamingAssetsPath,
        Custom
    }

    internal enum FileNameType
    {
        Time,
        Fixed
    }

    [System.Serializable]
    public class LogBaseConfig
    {
        /// <summary>
        /// 是否写入时间
        /// </summary> 
        [UnityEngine.SerializeField]
        private bool _writeTime;

        public bool WriteTime => _writeTime;

        [UnityEngine.SerializeField, LabelText("@DateTime.Now.ToString(_timeFormat)"), ShowIf("_writeTime")]
        private string _timeFormat = "yyyy-MM-dd HH:mm:ss fff";

        public string TimeFormat => _timeFormat;

        /// <summary>
        /// 是否写入堆栈信息
        /// </summary>
        [UnityEngine.SerializeField]
        private bool _writeTrace;

        public bool WriteTrace => _writeTrace;

        /// <summary>
        /// 是否写入线程ID
        /// </summary>
        [UnityEngine.SerializeField]
        private bool _writeThreadID;

        public bool WriteThreadID => _writeThreadID;

        /// <summary>
        /// 跳过堆栈帧数
        /// </summary>
        [UnityEngine.SerializeField]
        private int _skipTraceFrameCount;

        public int SkipTraceFrameCount => _skipTraceFrameCount;

        [UnityEngine.SerializeField, HideInInspector]
        private string[] _logColors = new string[3]
                                      {
                                          "#8DFF8D",
                                          "#FFFF8D",
                                          "#FF8D8D"
                                      };

        public string[] LogColors => _logColors;

#if UNITY_EDITOR
        [ShowInInspector, LabelText("Info颜色")]
        public Color InfoColor
        {
            get => ColorUtility.TryParseHtmlString(_logColors[0], out var color) ? color : Color.white;
            set => _logColors[0] = $"#{ColorUtility.ToHtmlStringRGB(value)}";
        }

        [ShowInInspector, LabelText("Warning颜色")]
        public Color WarningColor
        {
            get => ColorUtility.TryParseHtmlString(_logColors[1], out var color) ? color : Color.white;
            set => _logColors[1] = $"#{ColorUtility.ToHtmlStringRGB(value)}";
        }

        [ShowInInspector, LabelText("Error颜色")]
        public Color ErrorColor
        {
            get => ColorUtility.TryParseHtmlString(_logColors[2], out var color) ? color : Color.white;
            set => _logColors[2] = $"#{ColorUtility.ToHtmlStringRGB(value)}";
        }

#endif


        /// <summary>
        /// <para>配置logger的基础配置。</para>
        /// </summary>
        /// <param name="writeTime"></param>
        /// <param name="timeFormat"></param>
        /// <param name="writeTrace"></param>
        /// <param name="writeThreadID"></param>
        /// <param name="skipTraceFrameCount"></param>
        /// <param name="Colors">Log显示的颜色，顺序为Info、Warning、Error</param>
        public LogBaseConfig(bool writeTime, bool writeTrace = false, bool writeThreadID = false,
                             int skipTraceFrameCount = 5, string timeFormat = "yyyy-MM-dd HH:mm:ss fff",
                             string[] Colors = null)
        {
            _writeTime = writeTime;
            _writeTrace = writeTrace;
            _writeThreadID = writeThreadID;
            _skipTraceFrameCount = skipTraceFrameCount;

            if (writeTime)
                CheckTimeFormat(timeFormat);

            if (Colors == null)
                return;

            for (var i = 0; i < Colors.Length; i++)
            {
                if (i > _logColors.Length)
                    break;

                if (!string.Equals(Colors[i], string.Empty))
                {
                    _logColors[i] = Colors[i];
                }
            }
        }

        public void Update()
        {
        }

        private string CheckTimeFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
                return "yyyy-MM-dd HH:mm:ss fff";

            try
            {
                DateTime.Now.ToString(format);
                return format;
            }
            catch (Exception)
            {
                return "yyyy-MM-dd HH:mm:ss fff";
                throw new Exception("时间格式错误，请检查格式是否正确");
            }
        }
    }

    [System.Serializable]
    public class LogFileConfig
    {
        /// <summary>
        /// 是否需要保存在本地
        /// </summary>
        [UnityEngine.SerializeField, PropertyOrder(-2)]
        private bool _needSave = false;

        public bool NeedSave => _needSave;

        [ShowInInspector, ReadOnly, PropertyOrder(-1), ShowIfGroup("NeedSaveFunc"), LabelText("文件储存路径")]
        public string SavePath => GetFilePath();

        [SerializeField, ShowIfGroup("NeedSaveFunc")]
        private FilePathType _filePathType;

        [UnityEngine.SerializeField, ShowIf("ShowSavePath"), ShowIfGroup("NeedSaveFunc"),
         FolderPath(ParentFolder = "Assets/", AbsolutePath = true)]
        private string _savePath;

        [SerializeField, ShowIfGroup("NeedSaveFunc")]
        private FileNameType _fileNameType;

        [UnityEngine.SerializeField, ShowIfGroup("NeedSaveFunc"), LabelText("@FileName()")]
        private string _saveName;

        [UnityEngine.SerializeField, ShowIfGroup("NeedSaveFunc")]
        private ELogType _saveLogTypes;

        public ELogType SaveLogTypes => _saveLogTypes;

        public void UpdateFilePath()
        {
            switch (_filePathType)
            {
                case FilePathType.PersistentDataPath:
                    _savePath = Application.persistentDataPath + "/Log";
                    break;
                case FilePathType.DataPath:
                    _savePath = Application.dataPath + "/Log";
                    break;
                case FilePathType.StreamingAssetsPath:
                    _savePath = Application.streamingAssetsPath + "/Log";
                    break;
                case FilePathType.Custom:
                    break;
            }
        }

        public string GetFilePath()
        {
            string fileName = null;
            UpdateFilePath();
            
            switch (_fileNameType)
            {
                case FileNameType.Time:
                    fileName = $"log_{DateTime.Now.ToString(_saveName)}.log";
                    break;
                case FileNameType.Fixed:
                {
                    if (!_saveName.Contains("."))
                    {
                        fileName = _saveName + ".log";
                    }

                    break;
                }
                default:
                    fileName = "log.log";
                    break;
            }

            fileName = FileIOUtility.SanitizeFileName(fileName);
            var path = CheckPath(_savePath, fileName);

            return path;
        }

        private string CheckPath(string path, string fileName)
        {
            string totalPath = Path.Combine(path, fileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists(totalPath))
            {
                string newFileName = Path.GetFileNameWithoutExtension(fileName) + "(1)" +
                                     Path.GetExtension(fileName);
                totalPath = Path.Combine(path, newFileName);
            }
            else
            {
                totalPath = Path.Combine(path, fileName);
            }

            return totalPath;
        }

        private bool NeedSaveFunc()
        {
            return _needSave;
        }

        private bool ShowSavePath()
        {
            return _needSave && _filePathType == FilePathType.Custom;
        }

        private string FileName()
        {
            switch (_fileNameType)
            {
                case FileNameType.Time:
                    return $"时间格式: {DateTime.Now.ToString(_saveName)}";
                default:
                    return "文件名称";
            }
        }
    }

    public class LogWebConfig
    {
    }
}