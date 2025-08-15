using System;
using System.Collections;
using UnityEngine;

namespace Cheems
{
    public class PeriodSystem : PersistentMonoSingleton<PeriodSystem>
    {
        public static Action Tick000 = new Action(EmptyAction),
            Tick100                  = new Action(EmptyAction),
            Tick200                  = new Action(EmptyAction),
            Tick300                  = new Action(EmptyAction),
            Tick400                  = new Action(EmptyAction),
            Tick500                  = new Action(EmptyAction),
            FixedTick000             = new Action(EmptyAction),
            FixedTick100             = new Action(EmptyAction),
            FixedTick200             = new Action(EmptyAction),
            LateTick000              = new Action(EmptyAction),
            LateTick100              = new Action(EmptyAction),
            LateTick200              = new Action(EmptyAction);

        private static void EmptyAction()
        {
        }

        #region 生命周期相关

        private void Update()
        {
            Tick000();
            Tick100();
            Tick200();
            Tick300();
            Tick400();
            Tick500();
        }

        private void FixedUpdate()
        {
            FixedTick000();
            FixedTick100();
            FixedTick200();
        }

        private void LateUpdate()
        {
            LateTick000();
            LateTick100();
            LateTick200();
        }

        #endregion

        #region 协程

        /// <summary>
        /// 启动一个协程序
        /// </summary>
        public static Coroutine Start_Coroutine(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        /// <summary>
        /// 停止一个协程序
        /// </summary>
        public static void Stop_Coroutine(Coroutine routine)
        {
            Instance.StopCoroutine(routine);
        }

        /// <summary>
        /// 整个系统全部协程都会停止
        /// </summary>
        public static void StopAllCoroutine()
        {
            Instance.StopAllCoroutines();
        }

        #endregion
    }
}