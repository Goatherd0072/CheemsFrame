// #if UNITY_EDITOR
// using UnityEditor;
// using UnityEngine;
//
// namespace Cheems.DebugUtil
// {
//     [CustomEditor(typeof(LogSetting))]
//     public class LogSettingEditor : Editor
//     {
//         private SerializedProperty logType;
//         private SerializedProperty baseConfig;
//         private SerializedProperty fileConfig;
//
//         void OnEnable()
//         {
//             logType = this.serializedObject.FindProperty("logTypes");
//             baseConfig = this.serializedObject.FindProperty("baseConfig");
//             fileConfig = this.serializedObject.FindProperty("fileConfig");
//         }
//
//         public override void OnInspectorGUI()
//         {
//
//             // base.OnInspectorGUI();
//             // 更新显示
//             this.serializedObject.Update();
//
//             // 自定义绘制
//             LogTypeGUI();
//             BaseConfigGUI();
//
//             // 应用属性修改
//             this.serializedObject.ApplyModifiedProperties();
//         }
//
//         private void LogTypeGUI()
//         {
//             GUIContent logTContent = new("控制台输出的log类型:");
//             EditorGUILayout.PropertyField(logType, logTContent);
//         }
//
//         private void BaseConfigGUI()
//         {
//             EditorGUILayout.PropertyField(baseConfig, false);
//             if (baseConfig.isExpanded)
//             {
//                 EditorGUI.indentLevel++;
//                 SerializedProperty needTime = baseConfig.FindPropertyRelative("_writeTime");
//                 SerializedProperty needTrace = baseConfig.FindPropertyRelative("_writeTrace");
//                 SerializedProperty colors = baseConfig.FindPropertyRelative("_logColors");
//
//                 EditorGUILayout.Space();
//                 EditorGUILayout.PropertyField(needTime, new GUIContent("显示时间"));
//                 if (needTime.boolValue)
//                 {
//                     EditorGUILayout.PropertyField(baseConfig.FindPropertyRelative("_timeFormat"), new GUIContent("显示时间的格式,如yyyy-MM-dd HH:mm:ss"));
//                 }
//
//                 EditorGUILayout.Space();
//                 EditorGUILayout.PropertyField(needTrace, new GUIContent("显示堆栈信息"));
//                 if (needTrace.boolValue)
//                 {
//                     EditorGUILayout.PropertyField(baseConfig.FindPropertyRelative("_writeThreadID"), new GUIContent("显示线程ID"));
//                     EditorGUILayout.PropertyField(baseConfig.FindPropertyRelative("_skipTraceFrameCount"), new GUIContent("跳过堆栈帧数"));
//                 }
//
//                 EditorGUILayout.Space();
//                 EditorGUILayout.BeginVertical(GUI.skin.box);
//                 GUILayout.Box("自定义Log颜色,Debug颜色固定为白色");
//                 var tempColors = new Color[3];
//                 for (int i = 0, size = colors.arraySize; i < size; i++)
//                 {
//                     // 检索属性数组元素
//                     var element = colors.GetArrayElementAtIndex(i);
//                     _ = new Color();
//                     Color color;
//                     if (ColorUtility.TryParseHtmlString(element.stringValue, out color))
//                     {
//                         tempColors[i] = color;
//                     }
//
//                 }
//
//                 var elements = new Color[3];
//                 elements[0] = EditorGUILayout.ColorField("Info颜色", tempColors[0]);
//                 elements[1] = EditorGUILayout.ColorField("Warning颜色", tempColors[1]);
//                 elements[2] = EditorGUILayout.ColorField("Error颜色", tempColors[2]);
//
//                 UpdateColor(colors, elements);
//                 EditorGUILayout.EndVertical();
//
//                 EditorGUILayout.Space();
//                 EditorGUI.indentLevel--;
//             }
//         }
//         private void UpdateColor(SerializedProperty property, Color[] colors)
//         {
//             for (int i = 0; i < property.arraySize; i++)
//             {
//                 property.GetArrayElementAtIndex(i).stringValue = $"#{ColorUtility.ToHtmlStringRGB(colors[i])}";
//             }
//         }
//     }
// }
// #endif