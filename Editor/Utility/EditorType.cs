//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace Cheems.Editor
{
   
/// <summary>
/// 类型相关的实用函数。
/// </summary>
internal static class Type
{
    private static readonly string[] AssemblyNames =
    {
#if UNITY_2017_3_OR_NEWER
        "Cheems",
#endif
        "Assembly-CSharp"
    };

    private static readonly string[] EditorAssemblyNames =
    {
#if UNITY_2017_3_OR_NEWER
        "Cheems",
#endif
        "Assembly-CSharp-Editor"
    };


    /// <summary>
    /// 获取指定基类的所有子类的名称。
    /// </summary>
    /// <param name="typeBase">基类类型。</param>
    /// <returns>指定基类的所有子类的名称。</returns>
    internal static string[] GetTypeNames(System.Type typeBase)
    {
        return GetTypeNames(typeBase, AssemblyNames);
    }

    /// <summary>
    /// 获取指定基类的所有子类的名称。
    /// </summary>
    /// <param name="typeBase">基类类型。</param>
    /// <returns>指定基类的所有子类的名称。</returns>
    internal static string[] GetEditorTypeNames(System.Type typeBase)
    {
        return GetTypeNames(typeBase, EditorAssemblyNames);
    }

    private static string[] GetTypeNames(System.Type typeBase, string[] assemblyNames)
    {
        List<string> typeNames = new List<string>();
        foreach (string assemblyName in assemblyNames)
        {
            Assembly assembly = null;
            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch
            {
                continue;
            }

            if (assembly == null)
            {
                continue;
            }

            System.Type[] types = assembly.GetTypes();
            foreach (System.Type type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                {
                    typeNames.Add(type.FullName);
                }
            }
        }

        typeNames.Sort();
        return typeNames.ToArray();
    }
}
}