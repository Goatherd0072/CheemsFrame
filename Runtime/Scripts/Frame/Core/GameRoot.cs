using Cheems.Debug;
using Cheems.Pool;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cheems
{
    /// <summary>
    /// 游戏开始的根节点
    /// </summary>
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    [DefaultExecutionOrder(-20)]
    public class GameRoot : MonoSingleton<GameRoot>
    {
        public GameConfig  mainConfig;
        public GameManager currentMgr;
        public LogSetting  logSetting;

        protected override void OnInitialized()
        {
            // logSetting.CheckFileConfigPath();
            CLog.Init(logSetting); //初始化日志系统
            EventHandler.Init();   //初始化事件系统    
            base.OnInitialized();
            DOTween.SetTweensCapacity(500, 200);
            mainConfig.Init();
            InitPoolSystem();
            //  InitUISystem(); //初始化UI
            // _ = UISystem.Instance;
            PeriodSystem.CreateInstance();
            // EnsureInstance(PeriodSystem.Instance);
            // EnsureInstance(LevelManager.Instance);

            InitGameManager();
        }

        void OnApplicationQuit()
        {
            CLog.Dispose();
        }

        // private void EnsureInstance<T>(T instance) where T : ISingleton
        // {
        //     // instance.GetActive();
        // }

        // private void InitUISystem()
        // {
        //     if (FindAnyObjectByType<UISystem>() == null)
        //     {
        //         var go = Instantiate(GameConfig.UIConfig.UIPrefabs);
        //         go.name = nameof(UISystem);
        //     }
        //
        //     EnsureInstance(UISystem.Instance);
        // }

        private void InitGameManager()
        {
            if (currentMgr == null)
            {
                currentMgr = this.GetComponent<GameManager>();
                if (currentMgr == null)
                {
                    this.gameObject.AddComponent<GameManager>();
                }
            }

            currentMgr.Init();
        }

        private void InitPoolSystem()
        {
            PoolSystem.Init(transform); //初始化对象池系统
            // ResSystem.InitGameObjectPoolForAssetName("FoodGridUnit", 200, 200);
            // ResSystem.InitGameObjectPoolForAssetName("BoardUnit", 200, 200);
            // ResSystem.InitGameObjectPoolForAssetName("FoodCard", 10, 10);
        }

        #region Editor

#if UNITY_EDITOR
        public GameRoot()
        {
            EditorApplication.playModeStateChanged += (e) => { InitForEditor(); };
        }

        [InitializeOnLoadMethod]
        static void InitForEditor()
        {
            // // 当前是否要进行播放或准备播放中
            // if (EditorApplication.isPlayingOrWillChangePlaymode)
            // {
            //     return;
            // }
            // 确保在编辑器模式下调用该方法
            if (!EditorApplication.isCompiling)
            {
                return;
            }

            if (!Instance.transform.TryGetComponent(out GameConfig config))
            {
                config = Instance.gameObject.AddComponent<GameConfig>();
            }

            config.InitOnEditor();
            Instance.mainConfig = config;
        }
#endif

        #endregion
    }
}