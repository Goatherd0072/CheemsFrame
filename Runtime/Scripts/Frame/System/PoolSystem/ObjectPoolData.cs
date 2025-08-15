using System.Collections.Generic;

namespace Cheems
{
    /// <summary>
    /// 普通类 对象 对象池数据
    /// </summary>
    public class ObjectPoolData
    {
        #region ObjectPoolData持有的数据及初始化方法

        // 对象容器
        public readonly Queue<object> poolQueue;

        // 容量限制 -1代表无限
        public int maxCapacity = -1;

        public ObjectPoolData(int capacity = -1)
        {
            maxCapacity = capacity;
            if (maxCapacity == -1)
            {
                poolQueue = new Queue<object>();
            }
            else
            {
                poolQueue = new Queue<object>(capacity);
            }
        }

        public ObjectPoolData()
        {
            
        }

        #endregion

        #region ObjectPool数据相关操作

        /// <summary>
        /// 将对象放进对象池
        /// </summary>
        public bool PushObj(object obj)
        {
            // 检测是不是超过容量
            if (maxCapacity != -1 && poolQueue.Count >= maxCapacity)
            {
                return false;
            }

            poolQueue.Enqueue(obj);
            return true;
        }

        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <returns></returns>
        public object GetObj()
        {
            return poolQueue.Dequeue();
        }

        public ObjectPoolData Destroy(bool pushThisToPool = false)
        {
            poolQueue.Clear();
            maxCapacity = -1;
            return pushThisToPool ? this : null;
        }

        #endregion
    }
}