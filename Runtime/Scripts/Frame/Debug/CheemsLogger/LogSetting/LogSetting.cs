using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor;
#endif

namespace Cheems.Debug
{
    [CreateAssetMenu(fileName = "LogSetting", menuName = "Logger/LogBaseSetting", order = 11)]
    public class LogSetting : ScriptableObject
    {
        public ELogType logTypes = ELogType.Debug | ELogType.Info | ELogType.Warning | ELogType.Error;

        [SerializeField]
        public LogBaseConfig baseConfig = new(true, true);

        [SerializeField]
        public LogFileConfig fileConfig = new();

#if UNITY_EDITOR
        private readonly Dictionary<ELogType, string> _symbolsDic = new()
                                                                    {
                                                                        { ELogType.Debug, "ENABLE_LOG_DEBUG" },
                                                                        { ELogType.Info, "ENABLE_LOG_INFO" },
                                                                        { ELogType.Warning, "ENABLE_LOG_WARNING" },
                                                                        { ELogType.Error, "ENABLE_LOG_ERROR" },
                                                                    };

        public void InitOnEditor()
        {
            UpdateSymbol();
        }

        private void OpenLog()
        {
            // string path = Application.persistentDataPath + savePath;
            // path = path.Replace("/", "\\");
            // System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void UpdateSymbol()
        {
            foreach (var pair in _symbolsDic)
            {
                if ((logTypes & pair.Key) != 0)
                {
                    AddScriptCompilationSymbol(pair.Value);
                }
                else
                {
                    RemoveScriptCompilationSymbol(pair.Value);
                }
            }
        }

        // public void CheckFileConfigPath()
        // {
        //     fileConfig.UpdateFilePath();
        // }

        private void Awake()
        {
            UpdateSymbol();
            baseConfig.Update();
            fileConfig.UpdateFilePath();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            UpdateSymbol();
            baseConfig.Update();
            fileConfig.UpdateFilePath();
        }
#endif

#if UNITY_EDITOR
        /// <summary>
        /// 增加预处理指令
        /// </summary>
        public static void AddScriptCompilationSymbol(string name)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);

            string symbols = UnityEditor.PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
            //Log.Debug(symbols);

            if (!symbols.Contains(name))
            {
                symbols += ";" + name;
                UnityEditor.PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, symbols);
            }
        }

        /// <summary>
        /// 移除预处理指令
        /// </summary>
        public static void RemoveScriptCompilationSymbol(string name)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            // BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            // Use namedBuildTarget as needed
            string symbols = UnityEditor.PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
            if (symbols.Contains(name))
            {
                UnityEditor.PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget,
                                                                     symbols.Replace(name, string.Empty));
            }
        }
#endif
    }
}