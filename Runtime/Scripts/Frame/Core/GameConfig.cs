using UnityEngine;

namespace Cheems
{
    public class GameConfig : MonoBehaviour
    {
        public static GameConfig Instance;
        public        UIConfig   uiConfig;
        public static UIConfig   UIConfig => Instance.uiConfig;

        public GameConfig()
        {
            Instance = this;
        }

        public void Init()
        {
            uiConfig.InitUIDataDic();

            // await foodDataConfig.Init();
        }

#if UNITY_EDITOR

        // 用于在编辑器中初始化
        public void InitOnEditor()
        {
            if (uiConfig == null)
            {
                return;
            }

            uiConfig.InitUIDataDicOnEditor();
        }
#endif

        //     private void InitUIDataDicOnEditor()
        //     {
        //         // 获取所有程序集
        //         System.Reflection.Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
        //         Type baseType = typeof(UIWindowBase);
        //         // 遍历程序集
        //         foreach (System.Reflection.Assembly assembly in asms)
        //         {
        //             // 遍历程序集下的每一个类型
        //             try
        //             {
        //                 Type[] types = assembly.GetTypes();
        //                 foreach (Type type in types)
        //                 {
        //                     if (baseType.IsAssignableFrom(type)
        //                         && !type.IsAbstract)
        //                     {
        //                         var attributes = type.GetCustomAttributes<UIWindowDataAttribute>();
        //                         foreach (var attribute in attributes)
        //                         {
        //                             //Log.Debug(type.Name);
        //                             uiConfig.Add(attribute.windowKey,
        //                                 new UIWindowData(attribute.isCache, attribute.assetPath, attribute.layerNum));
        //                         }

        //                     }
        //                 }
        //             }
        //             catch (Exception)
        //             {
        //                 continue;
        //             }
        //         }
        //     }
    }
}