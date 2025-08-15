using Cheems.Pool;

namespace Cheems
{
    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class StateBase
    {
        protected FSM _fsm;

        /// <summary>
        /// 初始化内部数据，系统使用
        /// </summary>
        /// <param name="fsm"></param>
        public void InitInternalData(FSM fsm)
        {
            this._fsm = fsm;
        }

        /// <summary>
        /// 初始化状态
        /// 只在状态第一次创建时执行
        /// </summary>
        /// <param name="owner">宿主</param>
        public virtual void Init(IFSMOwner owner)
        {
        }

        /// <summary>
        /// 反初始化
        /// 不再使用时候，放回对象池时调用
        /// 把一些引用置空，防止不能被GC
        /// </summary>
        public virtual void UnInit()
        {
            _fsm = null;
            // // 放回对象池
            // this.ObjectPushPool();
            ObjectPushPool();
        }

        /// <summary>
        /// 状态进入
        /// 每次进入都会执行
        /// </summary>
        public virtual void Enter()
        {
        }

        /// <summary>
        /// 每个状态的Update，使用请在开始的手动时候加入到Mono的Update中。
        /// <para>例如：</para>  
        ///  <para>Enter:</para>
        ///  <para>PeriodSystem.Tick000 += this.Update;</para>
        ///  <para>Exit:</para>
        ///  <para>PeriodSystem.Tick000 -= this.Update;</para>
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// 状态退出
        /// </summary>
        public virtual void Exit()
        {
        }

        protected virtual void ObjectPushPool()
        {
            PoolSystem.PushObject(this);
        }

        public bool TryGetShareData<T>(string key, out T data)
        {
            return _fsm.TryGetShareData<T>(key, out data);
        }

        public void AddShareData(string key, object data)
        {
            _fsm.AddShareData(key, data);
        }

        public void RemoveShareData(string key)
        {
            _fsm.RemoveShareData(key);
        }

        public void UpdateShareData(string key, object data)
        {
            _fsm.UpdateShareData(key, data);
        }

        public void CleanShareData()
        {
            _fsm.CleanShareData();
        }

        public bool ContainsShareData(string key)
        {
            return _fsm.ContainsShareData(key);
        }
    }
}