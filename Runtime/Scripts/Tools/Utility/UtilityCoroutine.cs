﻿using System.Collections;
using UnityEngine;

namespace Cheems
{
    /// <summary>
    /// 协程工具，避免GC
    /// </summary>
    public static class UtilityCoroutine
    {
        private struct WaitForFrameStruct : IEnumerator
        {
            public object Current => null;

            public bool MoveNext() { return false; }

            public void Reset() { }
        }

        private static readonly WaitForEndOfFrame  _waitForEndOfFrame = new WaitForEndOfFrame();
        private static readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        public static WaitForEndOfFrame WaitForEndOfFrame()
        {
            return _waitForEndOfFrame;
        }
        public static WaitForFixedUpdate WaitForFixedUpdate()
        {
            return _waitForFixedUpdate;
        }
        public static IEnumerator WaitForSeconds(float time)
        {
            float currTime = 0;
            while (currTime < time)
            {
                currTime += Time.deltaTime;
                yield return new WaitForFrameStruct();
            }
        }

        public static IEnumerator WaitForSecondsRealtime(float time)
        {
            float currTime = 0;
            while (currTime < time)
            {
                currTime += Time.unscaledDeltaTime;
                yield return new WaitForFrameStruct();
            }
        }

        public static IEnumerator WaitForFrame()
        {
            yield return new WaitForFrameStruct();
        }
        public static IEnumerator WaitForFrames(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForFrameStruct();
            }
        }
    }
}
