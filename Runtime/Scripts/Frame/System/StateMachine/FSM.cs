using System;
using System.Collections.Generic;

namespace Cheems
{
    /// <summary>
    /// 状态机持有者
    /// </summary>
    public interface IFSMOwner
    {
    }

    /// <summary>
    /// 状态机控制器
    /// </summary>
    public class FSM
    {
        // 当前状态
        public Type CurrStateType { get; private set; } = null;

        // 当前生效中的状态
        public StateBase CurrStateObj { get; private set; }

        // 宿主
        private IFSMOwner _owner;

        //当前状态机 所有的状态 Key:状态枚举的值 Value:具体的状态
        private readonly Dictionary<Type, StateBase> _stateDic = new Dictionary<Type, StateBase>();

        //状态机共享数据
        private Dictionary<string, object> _stateShareDataDic;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="owner">宿主</param>
        /// <param name="enableStateShareData">启用状态共享数据，但是注意存在装箱和拆箱情况！</param>
        /// <typeparam name="T">初始状态类型</typeparam>
        public void Init<T>(IFSMOwner owner, bool enableStateShareData = false) where T : StateBase, new()
        {
            this._owner = owner;
            if (enableStateShareData && _stateShareDataDic == null)
                _stateShareDataDic = new Dictionary<string, object>();
            ChangeState<T>();
        }

        /// <summary>
        /// 初始化（无默认状态，状态机待机）
        /// </summary>
        /// <param name="owner">宿主</param>
        /// <param name="enableStateShareData">是否使用共享数据</param>
        public void Init(IFSMOwner owner, bool enableStateShareData = false)
        {
            if (enableStateShareData && _stateShareDataDic == null)
                _stateShareDataDic = new Dictionary<string, object>();
            this._owner = owner;
        }

        #region 状态

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">具体要切换到的状态脚本类型</typeparam>
        /// <param name="reCurrentState">新状态和当前状态一致的情况下，是否也要切换</param>
        /// <returns></returns>
        public bool ChangeState<T>(bool reCurrentState = false) where T : StateBase, new()
        {
            Type stateType = typeof(T);
            // 状态一致，并且不需要刷新状态，则切换失败
            if (stateType == CurrStateType && !reCurrentState) return false;

            // 退出当前状态
            if (CurrStateObj != null)
            {
                CurrStateObj.Exit();
            }

            // 进入新状态
            CurrStateObj = GetState<T>();
            CurrStateType = stateType;
            CurrStateObj.Enter();

            return true;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">具体要切换到的状态脚本类型</typeparam>
        /// <param name="stateType"</param>
        /// <param name="reCurrentState">新状态和当前状态一致的情况下，是否也要切换</param>
        /// <returns></returns>
        public bool ChangeState(Type stateType, bool reCurrentState = false)
        {
            // Type stateType = state.GetType();
            // 状态一致，并且不需要刷新状态，则切换失败
            if (stateType == CurrStateType && !reCurrentState) return false;

            // 退出当前状态
            if (CurrStateObj != null)
            {
                CurrStateObj.Exit();
            }

            // 进入新状态
            CurrStateObj = GetState(stateType);
            CurrStateType = stateType;
            CurrStateObj.Enter();

            return true;
        }

        /// <summary>
        /// 从对象池获取一个状态
        /// </summary>
        public StateBase GetState<T>() where T : StateBase, new()
        {
            Type stateType = typeof(T);
            if (_stateDic.TryGetValue(stateType, out var st)) return st;
            // StateBase state = ResSystem.GetOrNew<T>();
            T state = new();
            state.InitInternalData(this);
            state.Init(_owner);
            _stateDic.Add(stateType, state);
            return state;
        }

        /// <summary>
        /// 从对象池获取一个状态
        /// </summary>
        public StateBase GetState(Type stateType)
        {
            if (_stateDic.TryGetValue(stateType, out var st)) return st;

            // 使用 Activator 创建实例
            if (stateType.IsSubclassOf(typeof(StateBase)) || stateType == typeof(StateBase))
            {
                var state = (StateBase)Activator.CreateInstance(stateType);
                state.InitInternalData(this);
                state.Init(_owner);
                _stateDic.Add(stateType, state);
                return state;
            }

            throw new ArgumentException("Invalid state type", nameof(stateType));
        }

        /// <summary>
        /// 检测并替换该对象在字典中的状态
        /// </summary>
        private void CheckState<T>(T state) where T : StateBase, new()
        {
            Type stateType = typeof(T);
            if (_stateDic.ContainsKey(stateType))
            {
                _stateDic[stateType] = state;
                return;
            }

            // StateBase state = ResSystem.GetOrNew<T>();
            // T state = new();
            state.InitInternalData(this);
            state.Init(_owner);
            _stateDic.Add(stateType, state);
        }

        /// <summary>
        /// 停止工作
        /// 把所有状态都释放，但是StateMachine未来还可以工作
        /// </summary>
        public void Stop()
        {
            // 处理当前状态的额外逻辑
            if (CurrStateObj != null)
            {
                CurrStateObj.Exit();
                CurrStateObj = null;
            }

            CurrStateType = null;
            // 处理缓存中所有状态的逻辑
            foreach (var state in _stateDic.Values)
            {
                state.UnInit();
            }

            _stateDic.Clear();
        }

        #endregion

        #region 状态共享数据

        public bool TryGetShareData<T>(string key, out T data)
        {
            bool res = _stateShareDataDic.TryGetValue(key, out object stateData);
            if (res)
            {
                data = (T)stateData;
            }
            else
            {
                data = default(T);
            }

            return res;
        }

        public void AddShareData(string key, object data)
        {
            _stateShareDataDic.Add(key, data);
        }

        public bool RemoveShareData(string key)
        {
            return _stateShareDataDic.Remove(key);
        }

        public bool ContainsShareData(string key)
        {
            return _stateShareDataDic.ContainsKey(key);
        }

        public bool UpdateShareData(string key, object data)
        {
            if (ContainsShareData(key))
            {
                _stateShareDataDic[key] = data;
                return true;
            }
            else return false;
        }

        public void CleanShareData()
        {
            _stateShareDataDic?.Clear();
        }

        #endregion

        /// <summary>
        /// 销毁，宿主应该释放掉StateMachine的引用
        /// </summary>
        public void Destroy()
        {
            // 处理所有状态
            Stop();
            // 清除共享数据
            CleanShareData();
            // 放弃所有资源的引用
            _owner = null;
            // // 放进对象池
            // this.ObjectPushPool();
        }
    }
}