namespace Cheems
{
    public enum EEvent
    {
        #region UI相关 11开头 例如 E1101EndCurTurn

        E1101SetStoreNewOrder,
        E1102SetStashPanelCard,
        E1103CheckCardAffordable,
        E1104SetCurNutrient,
        E1105ShowCardHoverInfo,
        E1106HideCardHoverInfo,

        #endregion

        #region 食物卡牌相关 12开头

        E1201DisposeFood,
        E1202AddFoodOnBoard,

        #endregion

        #region 回合相关 13

        E1301ResetLevel,
        E1302SetLevelEnable,
        E1303EndCurTurn,
        E1304SendScore,
        E1305PlantCard,
        E1306PlayerGetDamage,
        E1307RefreshCard,
        E1310SetCardSelect,
        E1311SetLevelInfoBoard,

        #endregion

        #region 游戏状态相关 14

        E1401EnterMainMenu,
        E1402EnterLevelSelect,
        E1403EnterLevel,
        E1404EnterRogueLevel,
        E1405PauseGame,
        E1406GameOver,

        #endregion

        #region 角色属性相关 15

        E1501CalculateMinMultiplier,
        E1502SetCoinCount,
        E1503CannotAfford,
        E1504SetPlayerMoney,
        #endregion
    }
}