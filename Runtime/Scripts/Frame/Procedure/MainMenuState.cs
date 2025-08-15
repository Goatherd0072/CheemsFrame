namespace Cheems.Procedure
{
    public class MainMenuState : ProcedureBase
    {
        public override void Enter()
        {
            base.Enter();
            CLog.Debug("MainMenuState");

            InitUIPanel();
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void InitUIPanel()
        {
            // UISystem.Show<BtnGroupPanel>();
            // UISystem.Show<LevelInfoPanel>();
            // UISystem.Show<CardPanel>();
            // UISystem.Show<PlayerDataPanel>();

            // UISystem.Show<LevelSelectPanel>();
            // UISystem.Show<MainMenuPanel>();
        }
    }
}