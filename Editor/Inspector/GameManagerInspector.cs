//------------------------------------------------------------
// Game Framework
// 版权所有 © 2013-2021 Jiang Yin. 保留所有权利。
// 主页: https://gameframework.cn/
// 反馈: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cheems.Editor
{
    [CustomEditor(typeof(GameManager))]
    internal sealed class GameManagerInspector : CheemsInspector
    {
        // 序列化属性，用于存储可用的流程类型名称和入口流程类型名称
        private SerializedProperty _availableProcedureTypeNames = null;
        private SerializedProperty _entranceProcedureTypeName   = null;

        // 存储流程类型名称的数组和当前可用的流程类型名称列表
        private string[]     _procedureTypeNames                 = null;
        private List<string> _currentAvailableProcedureTypeNames = null;
        private int          _entranceProcedureIndex             = -1;

        // 在启用时初始化序列化属性并刷新类型名称
        private void OnEnable()
        {
            _availableProcedureTypeNames = serializedObject.FindProperty("_availableProcedureTypeNames");
            _entranceProcedureTypeName = serializedObject.FindProperty("_entranceProcedureTypeName");

            RefreshTypeNames();
        }

        // 绘制自定义检查器界面
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            GameManager t = (GameManager)target;

            // 显示入口流程无效的错误信息
            if (string.IsNullOrEmpty(_entranceProcedureTypeName.stringValue))
            {
                EditorGUILayout.HelpBox("入口流程无效。", MessageType.Error);
            }
            else if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("当前流程",
                                           t.CurrentEntryProcedure == null
                                               ? "无"
                                               : t.CurrentEntryProcedure.GetType().ToString());
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                GUILayout.Label("可用流程", EditorStyles.boldLabel);
                if (_procedureTypeNames.Length > 0)
                {
                    EditorGUILayout.BeginVertical("box");
                    {
                        foreach (string procedureTypeName in _procedureTypeNames)
                        {
                            bool selected = _currentAvailableProcedureTypeNames.Contains(procedureTypeName);
                            if (selected != EditorGUILayout.ToggleLeft(procedureTypeName, selected))
                            {
                                if (!selected)
                                {
                                    _currentAvailableProcedureTypeNames.Add(procedureTypeName);
                                    WriteAvailableProcedureTypeNames();
                                }
                                else if (procedureTypeName != _entranceProcedureTypeName.stringValue)
                                {
                                    _currentAvailableProcedureTypeNames.Remove(procedureTypeName);
                                    WriteAvailableProcedureTypeNames();
                                }
                            }
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    EditorGUILayout.HelpBox("没有可用的流程。", MessageType.Warning);
                }

                if (_currentAvailableProcedureTypeNames.Count > 0)
                {
                    EditorGUILayout.Separator();

                    int selectedIndex = EditorGUILayout.Popup("入口流程", _entranceProcedureIndex,
                                                              _currentAvailableProcedureTypeNames.ToArray());
                    if (selectedIndex != _entranceProcedureIndex)
                    {
                        _entranceProcedureIndex = selectedIndex;
                        _entranceProcedureTypeName.stringValue = _currentAvailableProcedureTypeNames[selectedIndex];
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("请先选择可用的流程。", MessageType.Info);
                }
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        // 编译完成后刷新类型名称
        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();
            RefreshTypeNames();
        }

        // 刷新类型名称
        private void RefreshTypeNames()
        {
            _procedureTypeNames = Type.GetTypeNames(typeof(ProcedureBase))
                                      .Where(typeName => typeName != typeof(ProcedureBase).FullName)
                                      .ToArray();

            ReadAvailableProcedureTypeNames();
            int oldCount = _currentAvailableProcedureTypeNames.Count;
            _currentAvailableProcedureTypeNames = _currentAvailableProcedureTypeNames
                                                  .Where(x => _procedureTypeNames.Contains(x)).ToList();
            if (_currentAvailableProcedureTypeNames.Count != oldCount)
            {
                WriteAvailableProcedureTypeNames();
            }
            else if (!string.IsNullOrEmpty(_entranceProcedureTypeName.stringValue))
            {
                _entranceProcedureIndex =
                    _currentAvailableProcedureTypeNames.IndexOf(_entranceProcedureTypeName.stringValue);
                if (_entranceProcedureIndex < 0)
                {
                    _entranceProcedureTypeName.stringValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        // 读取可用的流程类型名称
        private void ReadAvailableProcedureTypeNames()
        {
            _currentAvailableProcedureTypeNames = new List<string>();
            int count = _availableProcedureTypeNames.arraySize;
            for (int i = 0; i < count; i++)
            {
                _currentAvailableProcedureTypeNames.Add(
                    _availableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue);
            }
        }

        // 写入可用的流程类型名称
        private void WriteAvailableProcedureTypeNames()
        {
            _availableProcedureTypeNames.ClearArray();
            if (_currentAvailableProcedureTypeNames == null)
            {
                return;
            }

            _currentAvailableProcedureTypeNames.Sort();
            int count = _currentAvailableProcedureTypeNames.Count;
            for (int i = 0; i < count; i++)
            {
                _availableProcedureTypeNames.InsertArrayElementAtIndex(i);
                _availableProcedureTypeNames.GetArrayElementAtIndex(i).stringValue =
                    _currentAvailableProcedureTypeNames[i];
            }

            if (!string.IsNullOrEmpty(_entranceProcedureTypeName.stringValue))
            {
                _entranceProcedureIndex =
                    _currentAvailableProcedureTypeNames.IndexOf(_entranceProcedureTypeName.stringValue);
                if (_entranceProcedureIndex < 0)
                {
                    _entranceProcedureTypeName.stringValue = null;
                }
            }
        }
    }
}