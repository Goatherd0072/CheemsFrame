using System;
using System.Collections.Generic;

namespace Cheems
{
    public sealed class EventDispatcher
    {
        private readonly Dictionary<EEvent, IEventInfo> _eventDic = new();
        // public readonly Dictionary<string, int>     EventNameDic = new();
        
        #region 添加事件监听

        /// <summary>
        /// 添加无参事件
        /// </summary>
        public void AddEventListener(EEvent eEvent, Action action)
        {
            // 有没有对应的事件可以监听
            if (_eventDic.ContainsKey(eEvent))
            {
                (_eventDic[eEvent] as EventInfo).Action += action;
            }
            // 没有的话，需要新增 到字典中，并添加对应的Action
            else
            {
                // EventInfo eventInfo = objectPoolModule.GetObject<EventInfo>();
                // if (eventInfo == null) eventInfo = new EventInfo();
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                _eventDic.Add(eEvent, eventInfo);
            }
        }

        // <summary>
        // 添加1参事件监听
        // </summary>
        public void AddEventListener<TAction>(EEvent eEvent, TAction action) where TAction : MulticastDelegate
        {
            // 有没有对应的事件可以监听
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.Action = (TAction)Delegate.Combine(info.Action, action);
            }
            else AddMultipleParameterEventInfo(eEvent, action);
        }

        private void AddMultipleParameterEventInfo<TAction>(EEvent eEvent, TAction action)
            where TAction : MulticastDelegate
        {
            // MultipleParameterEventInfo<TAction> newEventInfo =
            //     objectPoolModule.GetObject<MultipleParameterEventInfo<TAction>>();
            // if (newEventInfo == null)
            var newEventInfo = new MultipleParameterEventInfo<TAction>();
            newEventInfo.Init(action);
            _eventDic.Add(eEvent, newEventInfo);
        }

        #endregion

        #region 移除事件监听

        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public void RemoveEventListener(EEvent eEvent, Action action)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((EventInfo)eventInfo).Action -= action;
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 移除有参数的事件监听
        /// </summary>
        public void RemoveEventListener<TAction>(EEvent eEvent, TAction action) where TAction : MulticastDelegate
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                MultipleParameterEventInfo<TAction> info = (MultipleParameterEventInfo<TAction>)eventInfo;
                info.Action = (TAction)Delegate.Remove(info.Action, action);
            }
        }

        #endregion

        #region 触发事件

        /// <summary>
        /// 触发无参的事件
        /// </summary>
        public void DispatchEvent(EEvent eEvent)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((EventInfo)eventInfo).Action?.Invoke();
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发1个参数的事件
        /// </summary>
        public void DispatchEvent<T>(EEvent eEvent, T arg)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T>>)eventInfo).Action?.Invoke(arg);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发2个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1>(EEvent eEvent, T0 arg0, T1 arg1)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1>>)eventInfo).Action?.Invoke(arg0, arg1);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发3个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2>>)eventInfo).Action?.Invoke(arg0, arg1, arg2);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发4个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发5个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发6个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            T5                                                   arg5)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发7个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                       arg4,
            T5                                                       arg5, T6 arg6)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发8个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                           arg4,   T5 arg5, T6 arg6, T7 arg7)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发9个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                               arg4,   T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发10个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>>)eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发11个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>)eventInfo).Action
                    ?.Invoke(
                        arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发12个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(EEvent eEvent, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>)eventInfo).Action
                    ?.Invoke(
                        arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发13个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(EEvent eEvent, T0 arg0,
            T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>)eventInfo)
                    .Action?.Invoke(
                        arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发14个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(EEvent eEvent, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>)
                    eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发15个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(EEvent eEvent,
            T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>)
                    eventInfo).Action?.Invoke(
                    arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        /// <summary>
        /// 触发16个参数的事件
        /// </summary>
        public void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(EEvent eEvent,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            if (_eventDic.TryGetValue(eEvent, out IEventInfo eventInfo))
            {
                ((MultipleParameterEventInfo<
                        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>)eventInfo).Action
                    ?.Invoke(
                        arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14,
                        arg15);
            }
            else
            {
               CLog.Error($"eEvent: {eEvent} not found.");
            }
        }

        #endregion

        #region 删除事件

        /// <summary>
        /// 移除/删除一个事件
        /// </summary>
        public void RemoveEvent(EEvent eEvent)
        {
            if (_eventDic.Remove(eEvent, out IEventInfo eventInfo))
            {
                eventInfo.Dispose();
            }
        }

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public void Clear()
        {
            foreach (IEventInfo eventInfo in _eventDic.Values)
            {
                eventInfo.Dispose();
            }

            _eventDic.Clear();
        }

        #endregion
    }

    #region 内部接口、类

    public interface IEventInfo
    {
        void Dispose();
    }

    /// <summary>
    /// 无参-事件信息
    /// </summary>
    public class EventInfo : IEventInfo
    {
        public Action Action;

        public void Init(Action action)
        {
            this.Action = action;
        }

        public void Dispose()
        {
            Action = null;
        }
    }

    /// <summary>
    /// 多参Action事件信息
    /// </summary>
    public class MultipleParameterEventInfo<TAction> : IEventInfo where TAction : MulticastDelegate
    {
        public TAction Action;

        public void Init(TAction action)
        {
            this.Action = action;
        }

        public void Dispose()
        {
            Action = null;
        }
    };

    #endregion
}