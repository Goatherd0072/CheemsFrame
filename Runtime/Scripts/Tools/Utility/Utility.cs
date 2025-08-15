using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace Cheems
{
    public static partial class Utility
    {
        static readonly Random rand = new Random();

        public static bool IsNull(this GameObject obj)
        {
            return ReferenceEquals(obj, null);
        }

        /// <summary>
        /// 计算枚举值中有多少个位被置为1
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int BitPopCount(Enum e)
        {
            int count = 0;
            ulong value = Convert.ToUInt64(e);

            while (value != 0)
            {
                count += (int)(value & 1);
                value >>= 1;
            }

            return count;
        }

        // 洗牌算法，使用 Fisher-Yates shuffle
        public static void ShuffleList<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static void SetCanvasGroupActive(this CanvasGroup canvasGroup, bool state, float during = 0.3f,
                                                Action onComplete = null)
        {
            if (during > 0)
            {
                canvasGroup.DOFade(state ? 1 : 0, during).onComplete = () =>
                                                                       {
                                                                           canvasGroup.blocksRaycasts = state;
                                                                           canvasGroup.interactable = state;
                                                                           onComplete?.Invoke();
                                                                       };
            }
            else
            {
                canvasGroup.alpha = state ? 1 : 0;
                canvasGroup.blocksRaycasts = state;
                canvasGroup.interactable = state;
                onComplete?.Invoke();
            }
            // canvasGroup.alpha = state ? 1 : 0;
        }

        /// <summary>
        /// 获取一个点的周围邻居点
        /// </summary>
        /// <param name="center"></param>
        /// <param name="interval"></param>
        /// <param name="containBevel">是否包含斜角</param>
        /// <returns></returns>
        public static List<Vector3> GetNeighbors2D(Vector3 center, int interval = 1, bool containBevel = false)
        {
            List<Vector3> neighbors = new List<Vector3>();

            // 遍历 x, y 方向上的 -1, 0, 1
            for (int x = -interval; x <= interval; x++)
            {
                for (int y = -interval; y <= interval; y++)
                {
                    // 排除中心点自身
                    if (x == 0 && y == 0)
                        continue;

                    // 排除斜角
                    if (!containBevel)
                    {
                        if (Mathf.Abs(x) == Mathf.Abs(y))
                        {
                            continue;
                        }
                    }

                    // 添加相邻点，仅在 x, y 平面上
                    Vector3 neighbor = center + new Vector3(x, y, 0);
                    neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        public static List<int> GetRandomNumbers(int count, int min, int max)
        {
            List<int> randomNumbers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int randomNumber = rand.Next(min, max); // Next(min, max) 生成[min, max)范围的随机数
                randomNumbers.Add(randomNumber);
            }

            return randomNumbers;
        }
    }
}