using UnityEngine;

namespace Cheems
{
    /// <summary>
    /// 2DGridSystem XY为正方向
    /// </summary>
    public class Grid2DSystem<T>
    {
        private int _width, _height;
        public  int Width  => _width;
        public  int Height => _height;

        /// <summary>
        /// grid的大小
        /// </summary>
        private float _gridSize;

        /// <summary>
        /// 左下角grid的坐标
        /// </summary>
        private Vector3 _bottomLeft;

        private T[,] _gridValues;
        
        public T[,] GridValues => _gridValues;

        public Grid2DSystem(int width, int height, float gridSize, Vector3 bottomLeft)
        {
            this._width = width;
            this._height = height;
            this._gridSize = gridSize;
            this._bottomLeft = bottomLeft;
            this._gridValues = new T[width, height];
        }

        public void SetValue(int x, int y, T value)
        {
            if (!IsValidGrid(x, y))
            {
               CLog.Error("GridSystem SetValue Error: x or y out of range");
                return;
            }

            _gridValues[x, y] = value;
        }

        public T GetValue(int x, int y)
        {
            if (!IsValidGrid(x, y))
            {
               CLog.Error("GridSystem GetValue Error: x or y out of range");
                return default;
            }

            return _gridValues[x, y];
        }

        public bool TryGetValue(int x, int y, out T value)
        {
            if (!IsValidGrid(x, y))
            {
                value = default;
                return false;
            }

            value = _gridValues[x, y];
            return true;
        }

        public bool TryGetValue(Vector3 pos, out T value)
        {
            var gridPos = TryGetGridPos(pos);
            if (!IsValidGrid(gridPos.x, gridPos.y))
            {
                value = default;
                return false;
            }
            //Log.Debug(gridPos);

            value = _gridValues[gridPos.x, gridPos.y];
            return true;
        }

        /// <summary>
        /// grid坐标是否有效
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsValidGrid(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height;
        }

        /// <summary>
        /// 通关Grid坐标获取grid的世界坐标（对应Unity的坐标）
        /// 中心坐标
        /// </summary>
        public Vector3 GetGridWorldPos(Vector2Int pos)
        {
            return GetGridWorldPos(pos.x, pos.y);
        }

        /// <summary>
        /// 通关Grid坐标获取grid的世界坐标（对应Unity的坐标）
        /// 中心坐标
        /// </summary>
        /// <param name="x">gridSystem中的坐标X</param>
        /// <param name="y">gridSystem中的坐标Y</param>
        /// <returns></returns>
        public Vector3 GetGridWorldPos(int x, int y)
        {
            if (!IsValidGrid(x, y))
            {
               CLog.Error("GridSystem GetGridWorldPos Error: x or y out of range");
                return Vector3.zero;
            }

            return new Vector3(_bottomLeft.x + x * _gridSize, _bottomLeft.y + y * _gridSize, _bottomLeft.z);
        }

        /// <summary>
        /// 通过世界坐标获取Grid坐标
        /// </summary>
        /// <param name="worldPos"></param>
        /// <returns></returns>
        public Vector2Int GetGridPos(Vector3 worldPos)
        {
            float x = worldPos.x - _bottomLeft.x;
            float y = worldPos.y - _bottomLeft.y;
            if (x < -_gridSize / 2f || x >= _width * _gridSize || y < -_gridSize / 2f || y >= _height * _gridSize)
            {
               CLog.Warning("The position don't in the gridsystem!");
                return new Vector2Int(-1, -1);
            }

            return new Vector2Int(Mathf.Clamp(Mathf.RoundToInt(x / _gridSize), 0, _width - 1),
                                  Mathf.Clamp(Mathf.RoundToInt(y / _gridSize), 0, _height - 1));
        }

        public Vector2Int TryGetGridPos(Vector3 worldPos)
        {
            float x = worldPos.x - _bottomLeft.x;
            float y = worldPos.y - _bottomLeft.y;
            if (x < -_gridSize / 2f || x >= _width * _gridSize || y < -_gridSize / 2f || y >= _height * _gridSize)
            {
                //Log.Warning("The position don't in the gridsystem!");
                return new Vector2Int(-1, -1);
            }

            return new Vector2Int(Mathf.Clamp(Mathf.RoundToInt(x / _gridSize), 0, _width - 1),
                                  Mathf.Clamp(Mathf.RoundToInt(y / _gridSize), 0, _height - 1));
        }

        public T GetValue(Vector3 worldPos)
        {
            Vector2Int gridPos = GetGridPos(worldPos);
            return GetValue(gridPos.x, gridPos.y);
        }

        /// <summary>
        /// 获取Grid中心点的世界坐标
        /// </summary>
        /// <returns></returns>
        public Vector3 GetGridCenterPointPos()
        {
            return GetGridWorldPos(_width - 1, _height - 1) / 2;
        }
    }
}