using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
#endif

namespace Cheems.Pool
{
    /// <summary>
    /// 对象池系统
    /// </summary>
    public static class PoolSystem
    {
        #region 对象池系统数据及静态构造方法

        private static GameObjectPoolModule GameObjectPoolModule;
        private static ObjectPoolModule     ObjectPoolModule;
        private static Transform            poolRootTransform;

        public static void Init(Transform gameRoot)
        {
            GameObjectPoolModule = new GameObjectPoolModule();
            ObjectPoolModule = new ObjectPoolModule();

            poolRootTransform = new GameObject("PoolRoot").transform;
            poolRootTransform.position = Vector3.zero;
            poolRootTransform.SetParent(gameRoot);

            GameObjectPoolModule.Init(poolRootTransform);
            ObjectPoolModule.InitObjectPool<ObjectPoolData>(-1, 0);
            ObjectPoolModule.InitObjectPool<GameObjectPoolData>(-1, 0);
            GameObjectPoolModule.InitObjectPool(GameObjectPoolData.CLEAR_POOL_ROOT_NAME, null, -1, 0);
        }

        #endregion

        #region GameObject对象池相关API(初始化、取出、放入、清空)

        /// <summary>
        /// 初始化一个GameObject类型的对象池类型
        /// </summary>
        /// <param name="keyName">资源名称</param>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        /// <param name="prefab">填写默认容量时预先放入的对象</param>
        public static void InitGameObjectPool(string keyName, GameObject prefab, int maxCapacity = -1,
                                              uint defaultQuantity = 0)
        {
            GameObjectPoolModule.InitObjectPool(keyName, prefab, maxCapacity, defaultQuantity);
        }

        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity">最大容量，-1代表无限</param>
        /// <param name="gameObjects">默认要放进来的对象数组</param>
        public static void InitGameObjectPool(string keyName, GameObject[] gameObjects, int maxCapacity = -1)
        {
            GameObjectPoolModule.InitObjectPool(keyName, gameObjects, maxCapacity);
        }


        /// <summary>
        /// 初始化对象池并设置容量
        /// </summary>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        /// <param name="prefab">填写默认容量时预先放入的对象</param>
        public static void InitGameObjectPool(GameObject prefab, int maxCapacity = -1, uint defaultQuantity = 0)
        {
            InitGameObjectPool(prefab.name, prefab, maxCapacity, defaultQuantity);
        }


        /// <summary>
        /// 获取GameObject，没有则返回Null
        /// </summary>
        public static GameObject GetGameObject(string keyName, Transform parent = null)
        {
            var go = GameObjectPoolModule.GetObject(keyName, parent);
            return go;
        }

        /// <summary>
        /// 获取GameObject，没有则返回Null
        /// T:组件
        /// </summary>
        public static T GetGameObject<T>(string keyName, Transform parent = null) where T : Component
        {
            var go = GetGameObject(keyName, parent);
            return go != null ? go.GetComponent<T>() : null;
        }

        /// <summary>
        /// 游戏物体放置对象池中
        /// </summary>
        /// <param name="keyName">对象池中的key</param>
        /// <param name="obj">放入的物体</param>
        public static bool PushGameObject(string keyName, GameObject obj)
        {
            if (!obj.IsNull())
            {
                bool res = GameObjectPoolModule.PushObject(keyName, obj);
                return res;
            }
            else
            {
                CLog.Error("您正在将Null放置对象池");
                return false;
            }
        }

        /// <summary>
        /// 游戏物体放置对象池中
        /// </summary>
        /// <param name="obj">放入的物体,并且基于它的name来确定它是什么物体</param>
        public static bool PushGameObject(GameObject obj)
        {
            return PushGameObject(obj.name, obj);
        }

        /// <summary>
        /// 清除某个游戏物体在对象池中的所有数据
        /// </summary>
        public static void ClearGameObject(string keyName)
        {
            var datas = GameObjectPoolModule.Clear(keyName);

            if (keyName == GameObjectPoolData.CLEAR_POOL_ROOT_NAME)
                return;

            if (datas.data != null)
            {
                PushObject(datas, typeof(GameObjectPoolData).FullName);
            }

            if (datas.rootGo != null)
            {
                PushGameObject(datas.rootGo);
            }
        }

        #endregion

        #region Object对象池相关API(初始化、取出、放入、清空)

        /// <summary>
        /// 初始化对象池并设置容量
        /// </summary>
        /// <param name="keyName">资源名称</param>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        public static void InitObjectPool<T>(string keyName, int maxCapacity = -1, int defaultQuantity = 0)
            where T : new()
        {
            ObjectPoolModule.InitObjectPool<T>(keyName, maxCapacity, defaultQuantity);
        }

        /// <summary>
        /// 初始化对象池并设置容量
        /// </summary>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        public static void InitObjectPool<T>(int maxCapacity = -1, int defaultQuantity = 0) where T : new()
        {
            InitObjectPool<T>(typeof(T).FullName, maxCapacity, defaultQuantity);
        }

        /// <summary>
        /// 初始化一个普通C#对象池类型
        /// </summary>
        /// <param name="keyName">keyName</param>
        /// <param name="maxCapacity">容量，超出时会丢弃而不是进入对象池，-1代表无限</param>
        public static void InitObjectPool(string keyName, int maxCapacity = -1)
        {
            ObjectPoolModule.InitObjectPool(keyName, maxCapacity);
        }

        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        public static void InitObjectPool(System.Type type, int maxCapacity = -1)
        {
            ObjectPoolModule.InitObjectPool(type, maxCapacity);
        }

        /// <summary>
        /// 获取普通对象（非GameObject）
        /// </summary>
        public static T GetObject<T>() where T : class
        {
            return GetObject<T>(typeof(T).FullName);
        }

        /// <summary>
        /// 获取普通对象（非GameObject）
        /// </summary>
        public static T GetObject<T>(string keyName) where T : class
        {
            object obj = GetObject(keyName);
            if (obj == null) return null;
            else return (T)obj;
        }

        /// <summary>
        /// 获取普通对象（非GameObject）
        /// </summary>
        public static object GetObject(System.Type type)
        {
            return GetObject(type.FullName);
        }

        /// <summary>
        /// 获取普通对象（非GameObject）
        /// </summary>
        public static object GetObject(string keyName)
        {
            object obj = ObjectPoolModule.GetObject(keyName);
            return obj;
        }

        /// <summary>
        /// 普通对象（非GameObject）放置对象池中
        /// 基于类型存放
        /// </summary>
        public static bool PushObject(object obj)
        {
            return PushObject(obj, obj.GetType().FullName);
        }

        /// <summary>
        /// 普通对象（非GameObject）放置对象池中
        /// 基于KeyName存放
        /// </summary>
        public static bool PushObject(object obj, string keyName)
        {
            if (obj == null)
            {
                CLog.Error("您正在将Null放置对象池");
                return false;
            }
            else
            {
                bool res = ObjectPoolModule.PushObject(obj, keyName);
                return res;
            }
        }

        /// <summary>
        /// 清理某个C#类型数据
        /// </summary>
        public static void ClearObject<T>()
        {
            ClearObject(typeof(T).FullName);
        }

        /// <summary>
        /// 清理某个C#类型数据
        /// </summary>
        public static void ClearObject(System.Type type)
        {
            ClearObject(type.FullName);
        }

        /// <summary>
        /// 清理某个C#类型数据
        /// </summary>
        public static void ClearObject(string keyName)
        {
            var data = ObjectPoolModule.ClearObject(keyName);
            if (data != null)
            {
                PushObject(data, typeof(ObjectPoolData).FullName);
            }
        }

        #endregion

        #region 对GameObject和Object对象池同时启用的API（清空全部）

        /// <summary>
        /// 清除全部
        /// </summary>
        public static void ClearAll(bool clearGameObject = true, bool clearCSharpObject = true)
        {
            if (clearGameObject)
            {
                GameObjectPoolModule.ClearAll();
                // while (poolRootTransform.childCount > 0)
                // {
                //     Object.Destroy(poolRootTransform.GetChild(0).gameObject);
                // }
            }

            if (clearCSharpObject)
            {
                ObjectPoolModule.ClearAll();
            }
        }

        #endregion


        #region Editor

#if UNITY_EDITOR
        public static Dictionary<string, GameObjectPoolData> GetGameObjectLayerDatas()
        {
            return GameObjectPoolModule.poolDic;
        }

        public static Dictionary<string, ObjectPoolData> GetObjectLayerDatas()
        {
            return ObjectPoolModule.poolDic;
        }
#endif

        #endregion
    }
}