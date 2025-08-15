using System;

namespace Cheems
{
    /// <summary>
    /// The singleton implementation for classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        protected Singleton()
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
            get => new Lazy<T>(() =>
                               {
                                   instance = new T();
                                   instance.InitializeSingleton();
                                   return instance;
                               }).Value;
        }
        // public static T Instance
        // {
        //     get
        //     {
        //         if (instance == null)
        //         {
        //             //ensure that only one thread can execute
        //             lock (typeof(T))
        //             {
        //                 if (instance == null)
        //                 {
        //                     instance = new T();
        //                     instance.InitializeSingleton();
        //                 }
        //             }
        //         }
        //
        //         return instance;
        //     }
        // }

        /// <summary>
        /// Gets whether the singleton's instance is initialized.
        /// </summary>
        public virtual bool IsInitialized => this._initializationStatus == ESingletonInitializationStatus.Initialized;

        #endregion

        #region Protected Methods

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