using System;
using System.Collections.Generic;
using System.Reflection;
using Cheems.UI;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
#endif


[CreateAssetMenu(fileName = "UIData", menuName = "UIData")]
public class UIConfig : SerializedScriptableObject
{
    public GameObject UIPrefabs;

    [SerializeField]
    private Dictionary<string, UIWindowData> dataDic = new();

    public Dictionary<string, UIWindowData> GetDataDic()
    {
        InitUIDataDic();
        return dataDic;
    }

    [Button("InitUIDataDic")]
    public void InitUIDataDic()
    {
        // 获取所有程序集
        System.Reflection.Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

        Type baseType = typeof(UIWindowBase);
        // 遍历程序集
        foreach (System.Reflection.Assembly assembly in asms)
        {
            // 遍历程序集下的每一个类型
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (baseType.IsAssignableFrom(type)
                        && !type.IsAbstract)
                    {
                        var attributes = type.GetCustomAttributes<UIWindowDataAttribute>();
                        foreach (var attribute in attributes)
                        {
                            // 检查键是否已经存在
                            if (!dataDic.ContainsKey(attribute.windowKey))
                            {
                                dataDic.Add(attribute.windowKey, new UIWindowData(attribute.isCache, attribute.assetPath, attribute.layerNum));
                            }
                            else
                            {
                                // 处理键重复的情况，可以选择覆盖或者跳过
                                //Log.Warning($"Key {attribute.windowKey} already exists. Skipping or updating as needed.");
                                // 如果你想覆盖现有值，可以使用下面这一行
                                dataDic[attribute.windowKey] = new UIWindowData(attribute.isCache, attribute.assetPath, attribute.layerNum);
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // 输出详细的异常信息以便调试
               Debug.LogError($"ReflectionTypeLoadException: {ex.Message}");
                foreach (var loaderException in ex.LoaderExceptions)
                {
                   Debug.LogError(loaderException);
                }
            }
            catch (Exception ex)
            {
               Debug.LogError($"Exception: {ex.Message}");
            }
        }
    }

#if UNITY_EDITOR
    public void InitUIDataDicOnEditor()
    {
        InitUIDataDic();
    }
#endif
}