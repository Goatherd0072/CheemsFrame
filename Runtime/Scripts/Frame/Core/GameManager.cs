//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections;
using Cheems.Input;
using Cheems.Procedure;
using Cheems.UI;
using UnityEngine;

namespace Cheems
{
    // public class GameManager : PersistentMonoSingleton<GameManager>, IFSMOwner
    public class GameManager : MonoBehaviour, IFSMOwner
    {
        protected FSM _gameMainStage = new();

        [SerializeField]
        private string[] _availableProcedureTypeNames = null;

        [SerializeField]
        private string _entranceProcedureTypeName = null;

        private ProcedureBase _entryEntryProcedure;

        public ProcedureBase CurrentEntryProcedure => _entryEntryProcedure;

        public void Init()
        {
            // EventHandler.AddEventListener(EEvent.E1401EnterMainMenu, ChangeToMainMenu);
            // EventHandler.AddEventListener(EEvent.E1402EnterLevelSelect, ChangeToLevelSelect);
            // EventHandler.AddEventListener<int>(EEvent.E1403EnterLevel, ChangeEnterLevel);
            // EventHandler.AddEventListener(EEvent.E1404EnterRogueLevel, EnterRogueLevel);
            // InitUISystem();
            InputManager.Instance.UpdateMainCamera();
            StartCoroutine(GetEntryProcedure());
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void InitUISystem()
        {
            if (FindAnyObjectByType<UISystem>() == null)
            {
                var go = Instantiate(GameConfig.UIConfig.UIPrefabs);
                go.name = nameof(UISystem);
            }

            // UISystem.Instance.GetActive();
            UISystem.CreateInstance();
        }
        
        public void Dispose()
        {
            _gameMainStage.Destroy();
            _gameMainStage = null;

            // EventHandler.RemoveEventListener(EEvent.E1401EnterMainMenu, ChangeToMainMenu);
            // // EventHandler.RemoveEventListener(EEvent.E1402EnterLevelSelect, ChangeToLevelSelect);
            // // EventHandler.RemoveEventListener<int>(EEvent.E1403EnterLevel, ChangeEnterLevel);
            // EventHandler.RemoveEventListener(EEvent.E1404EnterRogueLevel, EnterRogueLevel);
        }

        private void ChangeToMainMenu()
        {
            _gameMainStage.ChangeState<MainMenuState>();
        }
        
        private IEnumerator GetEntryProcedure()
        {
            // yield return new WaitForSeconds(0.1f);
            ProcedureBase[] procedures = new ProcedureBase[_availableProcedureTypeNames.Length];
            for (int i = 0; i < _availableProcedureTypeNames.Length; i++)
            {
                Type procedureType = Utility.Assembly.GetType(_availableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    CLog.Error($"Can not find procedure type '{_availableProcedureTypeNames[i]}'.");
                    yield break;
                }

                procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    CLog.Error($"Can not create procedure instance '{_availableProcedureTypeNames[i]}'.");
                    yield break;
                }

                if (_entranceProcedureTypeName == _availableProcedureTypeNames[i])
                {
                    _entryEntryProcedure = procedures[i];
                }
            }

            if (_entryEntryProcedure == null)
            {
                CLog.Error("Entrance procedure is invalid.");
                yield break;
            }

            _gameMainStage.Init(this, true);

            yield return new WaitForEndOfFrame();

            _gameMainStage.ChangeState(_entryEntryProcedure.GetType(), true);
            //Log.Debug(success);
        }
    }
}