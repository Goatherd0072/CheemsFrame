using System;

namespace Cheems
{
    /// <summary>
    /// 事件系统管理器
    /// </summary>
    public static class EventHandler
    {
        private static EventDispatcher dispatcher = new();

        public static void Init()
        {
            dispatcher = new();
        }

        /// <summary>
        /// 通过事件名获取事件类型
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private static EEvent GetEvent(string eventName)
        {
            return (EEvent)Enum.Parse(typeof(EEvent), eventName);
        }

        private static EEvent GetEvent(int eventID)
        {
            return (EEvent)eventID;
        }

        #region 添加事件的监听

        /// <summary>
        /// 添加无参事件
        /// </summary>
        public static void AddEventListener(EEvent eEvent, Action action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener(int eventID, Action action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener(string eventName, Action action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加1个参数事件
        /// </summary>
        public static void AddEventListener<T>(EEvent eEvent, Action<T> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T>(int eventID, Action<T> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T>(string eventName, Action<T> action)
        {
            dispatcher.AddEventListener<Action<T>>(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加2个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1>(EEvent eEvent, Action<T0, T1> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1>(int eventID, Action<T0, T1> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加3个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2>(EEvent eEvent, Action<T0, T1, T2> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2>(int eventID, Action<T0, T1, T2> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加4个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3>(EEvent eEvent, Action<T0, T1, T2, T3> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3>(int eventID, Action<T0, T1, T2, T3> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加5个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4>(EEvent eEvent, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4>(int eventID, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加6个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(int eventID, Action<T0, T1, T2, T3, T4, T5> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5>(string eventName,
            Action<T0, T1, T2, T3, T4, T5>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加7个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加8个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加9个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加10个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加11个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加12个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                                 action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加13个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                                  action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加14个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>                                 action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>                              action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加15个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            EEvent eEvent, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            int eventID, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 添加16个参数事件
        /// </summary>
        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            EEvent eEvent, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.AddEventListener(eEvent, action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            int eventID, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.AddEventListener(GetEvent(eventID), action);
        }

        public static void AddEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.AddEventListener(GetEvent(eventName), action);
        }

        #endregion

        #region 触发事件

        /// <summary>
        /// 触发无参的事件
        /// </summary>
        public static void DispatchEvent(EEvent eEvent)
        {
            dispatcher.DispatchEvent(eEvent);
        }

        public static void DispatchEvent(int eventID)
        {
            dispatcher.DispatchEvent(GetEvent(eventID));
        }

        public static void DispatchEvent(string eventName)
        {
            dispatcher.DispatchEvent(GetEvent(eventName));
        }

        /// <summary>
        /// 触发1个参数的事件
        /// </summary>
        public static void DispatchEvent<T>(EEvent eEvent, T arg)
        {
            dispatcher.DispatchEvent<T>(eEvent, arg);
        }

        public static void DispatchEvent<T>(int eventID, T arg)
        {
            dispatcher.DispatchEvent<T>(GetEvent(eventID), arg);
        }

        public static void DispatchEvent<T>(string eventName, T arg)
        {
            dispatcher.DispatchEvent<T>(GetEvent(eventName), arg);
        }

        /// <summary>
        /// 触发2个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1>(EEvent eEvent, T0 arg0, T1 arg1)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1);
        }

        public static void DispatchEvent<T0, T1>(int eventID, T0 arg0, T1 arg1)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1);
        }

        public static void DispatchEvent<T0, T1>(string eventName, T0 arg0, T1 arg1)
        {
            dispatcher.DispatchEvent(GetEvent(eventName), arg0, arg1);
        }

        /// <summary>
        /// 触发3个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2);
        }

        public static void DispatchEvent<T0, T1, T2>(int eventID, T0 arg0, T1 arg1, T2 arg2)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2);
        }

        public static void DispatchEvent<T0, T1, T2>(string eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            dispatcher.DispatchEvent<T0, T1, T2>(GetEvent(eventName), arg0, arg1, arg2);
        }

        /// <summary>
        /// 触发4个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3);
        }

        public static void DispatchEvent<T0, T1, T2, T3>(int eventID, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3);
        }

        public static void DispatchEvent<T0, T1, T2, T3>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3>(GetEvent(eventName), arg0, arg1, arg2, arg3);
        }

        /// <summary>
        /// 触发5个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4>(int eventID, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                      arg4)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4>(GetEvent(eventName), arg0, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// 触发6个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                          arg4,   T5 arg5)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5>(int eventID, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                       arg4,    T5 arg5)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                          arg4,      T5 arg5)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5>(GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// 触发7个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                              arg4,   T5 arg5, T6 arg6)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6>(int eventID, T0 arg0, T1 arg1, T2 arg2, T3 arg3,
            T4                                                           arg4,    T5 arg5, T6 arg6)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6>(string eventName, T0 arg0, T1 arg1, T2 arg2,
            T3                                                              arg3,      T4 arg4, T5 arg5, T6 arg6)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6>(GetEvent(eventName), arg0, arg1, arg2, arg3, arg4,
                                                                 arg5, arg6);
        }

        /// <summary>
        /// 触发8个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2,
            T3                                                                  arg3,
            T4                                                                  arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7>(int eventID, T0 arg0, T1 arg1, T2 arg2,
            T3                                                               arg3,
            T4                                                               arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7>(GetEvent(eventName), arg0, arg1, arg2, arg3, arg4,
                                                                     arg5, arg6,
                                                                     arg7);
        }

        /// <summary>
        /// 触发9个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(EEvent eEvent, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventID, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName, T0 arg0, T1 arg1,
            T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5,
                arg6, arg7, arg8);
        }

        /// <summary>
        /// 触发10个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(EEvent eEvent, T0 arg0, T1 arg1,
            T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventID, T0 arg0, T1 arg1, T2 arg2,
            T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// 触发11个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(EEvent eEvent, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventID, T0 arg0, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName, T0 arg0,
            T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// 触发12个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(EEvent eEvent, T0 arg0,
            T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventID, T0 arg0,
            T1 arg1,
            T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10,
                                     arg11);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// 触发13个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(EEvent eEvent, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11,
                                     arg12);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventID, T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10,
                                     arg11, arg12);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// 触发14个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(EEvent eEvent,
            T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11,
                                     arg12, arg13);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventID,
            T0 arg0,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10,
                                     arg11, arg12, arg13);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string eventName,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12,
                arg13);
        }

        /// <summary>
        /// 触发15个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(EEvent eEvent,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11,
                                     arg12, arg13, arg14);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int eventID,
            T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10,
                                     arg11, arg12, arg13, arg14);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            string eventName, T0  arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10    arg10,     T11 arg11, T12 arg12, T13 arg13, T14 arg14)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12,
                arg13,
                arg14);
        }

        /// <summary>
        /// 触发16个参数的事件
        /// </summary>
        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            EEvent eEvent,
            T0     arg0,  T1  arg1,  T2  arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11    arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            dispatcher.DispatchEvent(eEvent, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11,
                                     arg12, arg13, arg14, arg15);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            int eventID,
            T0  arg0,  T1  arg1,  T2  arg2,  T3  arg3,  T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            dispatcher.DispatchEvent(GetEvent(eventID), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9,
                                     arg10,
                                     arg11, arg12, arg13, arg14, arg15);
        }

        public static void DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            string eventName, T0  arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            T10    arg10,     T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
        {
            dispatcher.DispatchEvent<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                GetEvent(eventName), arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12,
                arg13,
                arg14, arg15);
        }

        #endregion

        #region 取消事件的监听

        /// <summary>
        /// 移除无参的事件监听
        /// </summary>
        public static void RemoveEventListener(EEvent eEvent, Action action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener(int eventID, Action action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener(string eventName, Action action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除1个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T>(EEvent eEvent, Action<T> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T>(int eventID, Action<T> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T>(string eventName, Action<T> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除2个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1>(EEvent eEvent, Action<T0, T1> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1>(int eventID, Action<T0, T1> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除3个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2>(EEvent eEvent, Action<T0, T1, T2> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2>(int eventID, Action<T0, T1, T2> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除4个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3>(EEvent eEvent, Action<T0, T1, T2, T3> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3>(int eventID, Action<T0, T1, T2, T3> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除5个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4>(EEvent eEvent, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4>(int eventID, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除6个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(int eventID,
            Action<T0, T1, T2, T3, T4, T5>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5>(string eventName,
            Action<T0, T1, T2, T3, T4, T5>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除7个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除8个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除9个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除10个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除11个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除12个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除13个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(EEvent eEvent,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                                    action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string eventName,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>                                    action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除14个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            EEvent eEvent, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int eventID,
            Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>                                 action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除15个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            EEvent eEvent, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            int eventID, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        /// <summary>
        /// 移除16个参数的事件监听
        /// </summary>
        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            EEvent eEvent, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.RemoveEventListener(eEvent, action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            int eventID, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventID), action);
        }

        public static void RemoveEventListener<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            string eventName, Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            dispatcher.RemoveEventListener(GetEvent(eventName), action);
        }

        #endregion

        #region 移除事件

        /// <summary>
        /// 移除/删除一个事件
        /// </summary>
        public static void RemoveEvent(EEvent eEvent)
        {
            dispatcher.RemoveEvent(eEvent);
        }

        public static void RemoveEvent(int eventID)
        {
            dispatcher.RemoveEvent(GetEvent(eventID));
        }

        public static void RemoveEvent(string eventName)
        {
            dispatcher.RemoveEvent(GetEvent(eventName));
        }

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public static void Clear()
        {
            dispatcher.Clear();
        }

        #endregion
    }
}