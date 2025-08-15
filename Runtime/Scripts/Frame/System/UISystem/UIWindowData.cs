using UnityEngine;

namespace Cheems.UI
{
    /// <summary>
    /// UI元素数据
    /// </summary>
    [System.Serializable]
    public class UIWindowData
    {
        // [LabelText("是否需要缓存")] 
        [SerializeField] public bool isCache;
        // [LabelText("预制体Path或AssetKey")] 
        [SerializeField] public string assetPath;
        //[LabelText("UI层级")] 
        [SerializeField] public int layerNum;
        /// <summary>
        /// 这个元素的窗口对象
        /// </summary>
        // [LabelText("窗口实例")] 
        [SerializeField] public UIWindowBase instance;

        public UIWindowData(bool isCache, string assetPath, int layerNum)
        {
            this.isCache = isCache;
            this.assetPath = assetPath;
            this.layerNum = layerNum;
            instance = null;
        }
    }
}