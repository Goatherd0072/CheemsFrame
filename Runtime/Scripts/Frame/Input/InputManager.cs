using UnityEngine;

namespace Cheems.Input
{
    public class InputManager : Singleton<InputManager>
    {
        private Camera _mainCam;
        // private FSM _inputFSM = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _mainCam = Camera.main;
        }
        
        public void UpdateMainCamera()
        {
            _mainCam = Camera.main;
        }

        /// <summary>
        /// 鼠标的屏幕坐标
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMouseScreenPosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        /// <summary>
        /// 鼠标的世界坐标，无Z轴
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMouseWorldPosition()
        {
            return _mainCam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
        }

        public Ray GetMouseRay()
        {
            return _mainCam.ScreenPointToRay(UnityEngine.Input.mousePosition);
        }
    }
}