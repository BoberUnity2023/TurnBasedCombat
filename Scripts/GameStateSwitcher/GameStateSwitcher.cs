using System;
using System.Collections.Generic;
using Anthill.Extensions;
using UnityEngine;

namespace GameState
{
    public class GameStateSwitcher : MonoBehaviour
    {
        private readonly List<ISwitchWhenGameStateChanges> _switchWhenGameStateChanges = new List<ISwitchWhenGameStateChanges>();

        public void TryAdd(ISwitchWhenGameStateChanges switchWhenGameStateChanges)
        {
            if (_switchWhenGameStateChanges.TryAdd(switchWhenGameStateChanges))
            {
                _switchWhenGameStateChanges.Add(switchWhenGameStateChanges);
            }
        }

        public void TryRemove(ISwitchWhenGameStateChanges switchWhenGameStateChanges)
        {
            if (!_switchWhenGameStateChanges.TryAdd(switchWhenGameStateChanges))
            {
                _switchWhenGameStateChanges.Remove(switchWhenGameStateChanges);
            }
        }

        public void SwitchGameOnTurnBase()
        {
            for (int i = 0; i < _switchWhenGameStateChanges.Count; i++)
            {
                _switchWhenGameStateChanges[i].SwitchOnTurnBase();
            }
        }

        public void SwitchGameOnRuntime()
        {
            for (int i = 0; i < _switchWhenGameStateChanges.Count; i++)
            {
                _switchWhenGameStateChanges[i].SwitchOnRuntime();
            }
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                SwitchGameOnRuntime();
            }
            
            if (Input.GetKeyDown(KeyCode.F8))
            {
                SwitchGameOnTurnBase();
            }
        }
    }
}
