using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cheems.Debug
{
    using Input = UnityEngine.Input;

    public class GameMaster : MonoBehaviour
    {
        private          bool           _showMenu   = false;
        private readonly List<MenuItem> _menuLayers = new List<MenuItem>(); // 存储每一层的菜单
        private          MenuItem       _mainMenu;                          // 主菜


        void Start()
        {
            // 创建并配置主菜单
            _mainMenu = new MenuItem("Main Menu");

            // 配置子菜单和功能
            var gameplayMenu = new MenuItem("Gameplay");
            // gameplayMenu.AddSubItem(new MenuItem("Speed Boost", () =>Log.Debug("Speed Boost Activated")));

            // 添加子菜单到主菜单
            _mainMenu.AddSubItem(gameplayMenu);

            // 初始化第一个菜单层为主菜单
            _menuLayers.Add(_mainMenu);
        }


        void Update()
        {
            // 检测按下`~`键，打开或关闭菜单
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                _showMenu = !_showMenu;
            }

            // 按下返回键返回上一级菜单
            if (_showMenu && Input.GetKeyDown(KeyCode.Escape))
            {
                if (_menuLayers.Count > 1)
                {
                    _menuLayers.RemoveAt(_menuLayers.Count - 1); // 返回上一级菜单
                }
                else
                {
                    _showMenu = false; // 关闭菜单
                }
            }
        }

        void OnGUI()
        {
            if (_showMenu)
            {
                // 显示每一层的菜单，每层在不同的位置
                for (int i = 0; i < _menuLayers.Count; i++)
                {
                    float xOffset = 10 + i * 220; // 每一层菜单的位置从左到右依次排列
                    DisplayMenu(_menuLayers[i], xOffset);
                }
            }
        }

        void DisplayMenu(MenuItem menu, float xOffset)
        {
            GUILayout.BeginArea(new Rect(xOffset, 10, 200, 300), GUI.skin.box);

            // 显示返回按钮和标题
            GUILayout.BeginHorizontal();
            if (menu != _mainMenu && GUILayout.Button("←", GUILayout.Width(30)))
            {
                _menuLayers.RemoveAt(_menuLayers.Count - 1); // 返回上一级菜单
            }

            if ((menu == _mainMenu) && GUILayout.Button("X", GUILayout.Width(30)))
            {
                _showMenu = false;
            }

            GUILayout.Label(menu.Title, GUILayout.Width(150));
            GUILayout.EndHorizontal();

            // 分页逻辑
            int totalItems = menu.SubItems.Count;
            int itemsPerPage = 8; // 每页显示的项目数
            int maxPages = Mathf.CeilToInt((float)totalItems / itemsPerPage);

            // 防止超出页码范围
            if (menu.CurrentPage >= maxPages) menu.CurrentPage = maxPages - 1;
            if (menu.CurrentPage < 0) menu.CurrentPage = 0;

            // 滚动区域和分页显示
            // Vector2 scrollPosition = Vector2.zero;
            // scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(450));

            for (int i = menu.CurrentPage * itemsPerPage;
                 i < Mathf.Min((menu.CurrentPage + 1) * itemsPerPage, totalItems);
                 i++)
            {
                var item = menu.SubItems[i];
                if (GUILayout.Button(item.Title))
                {
                    if (item.HasSubMenu)
                    {
                        if (_menuLayers.Count > _menuLayers.IndexOf(menu) + 1)
                        {
                            _menuLayers[_menuLayers.IndexOf(menu) + 1] = item;
                        }
                        else
                        {
                            _menuLayers.Add(item);
                        }
                    }
                    else
                    {
                        item.Execute();
                    }
                }
            }

            // GUILayout.EndScrollView();

            // 翻页按钮
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Pre", GUILayout.Width(90)) && menu.CurrentPage > 0)
            {
                menu.CurrentPage--; // 上一页
            }

            if (GUILayout.Button("Next", GUILayout.Width(90)) && menu.CurrentPage < maxPages - 1)
            {
                menu.CurrentPage++; // 下一页
            }

            GUILayout.EndHorizontal();


            GUILayout.EndArea();
        }

        // MenuItem类表示菜单项，可以包含子菜单或执行功能
        public class MenuItem
        {
            public string         Title       { get; private set; }
            public Action         Action      { get; private set; }
            public List<MenuItem> SubItems    { get; private set; } = new List<MenuItem>();
            public int            CurrentPage { get; set; }         = 0;

            // 构造函数，用于创建有功能的菜单项
            public MenuItem(string title, Action action = null)
            {
                Title = title;
                Action = action;
            }

            // 检查该菜单项是否有子菜单
            public bool HasSubMenu => SubItems.Count > 0;

            // 执行该菜单项的功能
            public void Execute()
            {
                Action?.Invoke();
            }

            // 添加子菜单
            public void AddSubItem(MenuItem subItem)
            {
                SubItems.Add(subItem);
            }
        }
    }
}