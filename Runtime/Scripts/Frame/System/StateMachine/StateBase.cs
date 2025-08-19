using System;
using Cheems.Pool;

namespace Cheems
{
    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class StateBase
    {
        protected FSM _fsm;

        #region 初始化和重置

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
        public virtual void Reset()
        {
            _fsm = null;
            // 放回对象池
            ObjectPushPool();
        }

        #endregion

        #region 状态控制

        /// <summary>
        /// 状态进入
        /// 每次进入都会执行
        /// </summary>
        public virtual void Enter()
        {
            AddUpdateEvent();
        }

        /// <summary>
        /// 每个状态的Update，使用请在开始的手动时候加入到Mono的Update中,或者UniTask
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
            RemoveUpdateEvent();
        }
        
        /// <summary>
        /// 状态过渡退出，默认调用Exit()
        /// 需要特殊过渡逻辑时可重写此方法
        /// </summary>
        public virtual void OnTransitionOut(Type toStateType)
        {
            Exit();
        }

        /// <summary>
        /// 状态过渡进入，默认调用Enter()  
        /// 需要特殊过渡逻辑时可重写此方法
        /// </summary>
        public virtual void OnTransitionIn(Type fromStateType)
        {
            Enter();
        }

        protected virtual void ObjectPushPool()
        {
            PoolSystem.PushObject(this);
        }

        /// <summary>
        /// 激活Update事件
        /// </summary>
        protected virtual void AddUpdateEvent()
        {
        }

        /// <summary>
        /// 移除Update事件
        /// </summary>
        protected virtual void RemoveUpdateEvent()
        {
        }

        #endregion

        #region ShareData

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

        #endregion
    }
}