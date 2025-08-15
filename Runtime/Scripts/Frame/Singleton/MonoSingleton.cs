using System.Linq;
using UnityEngine;

namespace Cheems
{
    /// <summary>
    /// The basic MonoBehaviour singleton implementation, this singleton is destroyed after scene changes, use <see cref="PersistentMonoSingleton{T}"/> if you want a persistent and global singleton instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        protected MonoSingleton()
        {
        }

        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;

        /// <summary>
        /// The initialization status of the singleton's instance.
        /// </summary>
        private ESingletonInitializationStatus _initializationStatus = ESingletonInitializationStatus.None;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
#if UNITY_6000_0_OR_NEWER
                    instance = FindFirstObjectByType<T>();
#else
                    instance = FindObjectOfType<T>();
#endif
                    if (instance == null)
                    {
                        GameObject obj = new ()
                                         {
                                             name = typeof(T).Name
                                         };
                        instance = obj.AddComponent<T>();
                        instance.InitializeSingleton();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => this._initializationStatus == ESingletonInitializationStatus.Initialized;

        #endregion

        #region Unity Messages

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;

                // Initialize existing instance
                InitializeSingleton();
            }
            else if (instance != this)
            {
                // Destory duplicates
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            DestroyInstance();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// This gets called once the singleton's instance is created.
        /// </summary>
        protected virtual void OnInitializing()
        {
        }

        protected virtual void OnInitialized()
        {
        }

        #endregion

        #region Public Methods

        public virtual void InitializeSingleton()
        {
            if (this._initializationStatus != ESingletonInitializationStatus.None)
            {
                return;
            }

            this._initializationStatus = ESingletonInitializationStatus.Initializing;
            OnInitializing();
            this._initializationStatus = ESingletonInitializationStatus.Initialized;
            OnInitialized();
        }

        public virtual void ClearSingleton()
        {
            Destroy(instance);

            var components = GetComponents<Component>().Where(c => c is not Transform).ToArray();
            if (transform.childCount <= 0 || components.Length <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public static void CreateInstance()
        {
            DestroyInstance();
            instance = Instance;
        }

        public static void DestroyInstance()
        {
            if (instance == null)
            {
                return;
            }

            instance.ClearSingleton();

            instance = null;
        }

        #endregion
    }
}