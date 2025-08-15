using System.Collections.Generic;
using UnityEngine;

namespace Cheems.Pool
{
    public class GameObjectPoolModule
    {
        #region GameObjectPoolModule持有的数据及初始化方法

        // 根节点
        private Transform _poolRoot;

        /// <summary>
        /// GameObject对象容器
        /// </summary>
        public readonly Dictionary<string, GameObjectPoolData> poolDic = new();

        public void Init(Transform poolRootTransform)
        {
            this._poolRoot = poolRootTransform;
        }

        /// <summary>
        /// 初始化对象池并设置容量
        /// </summary>
        /// <param name="keyName">资源名称</param>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        /// <param name="prefab">填写默认容量时预先放入的对象</param>
        public void InitObjectPool(string keyName, GameObject prefab, int maxCapacity = -1,
                                   uint defaultQuantity = 0)
        {
            if (defaultQuantity > maxCapacity && maxCapacity != -1)
            {
                CLog.Error("默认容量超出最大容量限制");
                return;
            }

            //设置的对象池已经存在
            if (poolDic.TryGetValue(keyName, out var poolData))
            {
                //更新容量限制
                poolData.maxCapacity = maxCapacity;
                //底层Queue自动扩容这里不管

                //在指定默认容量和默认对象时才有意义
                if (defaultQuantity > 0)
                {
                    if (prefab.IsNull() == false)
                    {
                        int nowCapacity = poolData.poolQueue.Count;
                        // 生成差值容量个数的物体放入对象池
                        for (int i = 0; i < defaultQuantity - nowCapacity; i++)
                        {
                            GameObject go = Object.Instantiate(prefab);
                            go.name = prefab.name;
                            poolData.PushObj(go);
                        }
                    }
                    else
                    {
                        CLog.Error("默认对象未指定");
                    }
                }
            }
            //设置的对象池不存在
            else
            {
                //创建对象池
                poolData = CreateGameObjectPoolData(keyName, maxCapacity);

                //在指定默认容量和默认对象时才有意义
                if (defaultQuantity != 0)
                {
                    if (prefab.IsNull() == false)
                    {
                        // 生成容量个数的物体放入对象池
                        for (int i = 0; i < defaultQuantity; i++)
                        {
                            GameObject go = GameObject.Instantiate(prefab);
                            go.name = prefab.name;
                            poolData.PushObj(go);
                        }
                    }
                    else
                    {
                        CLog.Error("默认容量或默认对象未指定");
                    }
                }
            }
        }

        /// <summary>
        /// 初始化对象池并设置容量
        /// </summary>
        /// <param name="maxCapacity">容量限制，超出时会销毁而不是进入对象池，-1代表无限</param>
        /// <param name="defaultQuantity">默认容量，填写会向池子中放入对应数量的对象，0代表不预先放入</param>
        /// <param name="prefab">填写默认容量时预先放入的对象</param>
        public void InitObjectPool(GameObject prefab, int maxCapacity = -1, uint defaultQuantity = 0)
        {
            InitObjectPool(prefab.name, prefab, maxCapacity, defaultQuantity);
        }


        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="maxCapacity">最大容量，-1代表无限</param>
        /// <param name="gameObjects">默认要放进来的对象数组</param>
        public void InitObjectPool(string keyName, GameObject[] gameObjects, int maxCapacity = -1)
        {
            if (gameObjects.Length > maxCapacity && maxCapacity != -1)
            {
                CLog.Error("默认容量超出最大容量限制");
                return;
            }

            //设置的对象池已经存在
            if (poolDic.TryGetValue(keyName, out GameObjectPoolData poolData))
            {
                //更新容量限制
                poolData.maxCapacity = maxCapacity;
            }
            //设置的对象池不存在
            else
            {
                //创建对象池
                poolData = CreateGameObjectPoolData(keyName, maxCapacity);
            }

            //数组大小为默认容量，数组内容作为默认对象置入对象池
            if (gameObjects.Length > 0)
            {
                int nowCapacity = poolData.poolQueue.Count;
                // 生成差值容量个数的物体放入对象池
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    if (i < gameObjects.Length - nowCapacity)
                    {
                        gameObjects[i].gameObject.name = keyName;
                        poolData.PushObj(gameObjects[i].gameObject);
                    }
                    else
                    {
                        Object.Destroy(gameObjects[i].gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// 创建一条新的对象池数据
        /// </summary>
        private GameObjectPoolData CreateGameObjectPoolData(string layerName, int maxCapacity = -1)
        {
            //交由Object对象池拿到poolData的类
            GameObjectPoolData poolData = PoolSystem.GetObject<GameObjectPoolData>();

            //Object对象池中没有再new
            if (poolData == null) poolData = new GameObjectPoolData(maxCapacity);

            //对拿到的poolData副本进行初始化（覆盖之前的数据）
            poolData.Init(layerName, _poolRoot, maxCapacity);
            poolDic.Add(layerName, poolData);
            return poolData;
        }

        #endregion

        #region GameObjectPool相关功能

        public GameObject GetObject(string keyName, Transform parent = null)
        {
            GameObject obj = null;
            // 检查有没有这一层
            if (poolDic.TryGetValue(keyName, out GameObjectPoolData poolData) && poolData.poolQueue.Count > 0)
            {
                obj = poolData.GetObj(parent);
            }

            return obj;
        }

        public void PushObject(GameObject go)
        {
            PushObject(go.name, go);
        }

        public bool PushObject(string keyName, GameObject obj)
        {
            // 现在有没有这一层
            if (poolDic.TryGetValue(keyName, out GameObjectPoolData poolData))
            {
                return poolData.PushObj(obj);
            }
            else
            {
                poolData = CreateGameObjectPoolData(keyName);
                return poolData.PushObj(obj);
            }
        }

        public (GameObjectPoolData data, GameObject rootGo) Clear(string keyName)
        {
            GameObject rootGo = null;
            if (poolDic.TryGetValue(keyName, out GameObjectPoolData gameObjectPoolData))
            {
                rootGo = gameObjectPoolData.DestroyObj(true);
                poolDic.Remove(keyName);
            }

            return (gameObjectPoolData, rootGo);
        }

        public void ClearAll()
        {
            var enumerator = poolDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumerator.Current.Value.DestroyObj(false);
            }

            poolDic.Clear();
        }

        #endregion
    }
}