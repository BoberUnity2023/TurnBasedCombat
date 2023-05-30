using System.Collections.Generic;
using AI;
using Anthill.Extensions;
using Anthill.Inject;
using Components.Player.Movement;
using DNVMVC;
using DNVMVC.Controllers;
using GameState;
using Libraries.DNV.MVC.Core;
using UnityEngine;

namespace Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private PlayerCombatSystem _playerCombatSystem;
        [SerializeField] private float _triggerRadius;
        [SerializeField] private List<EnemyAI> _enemyAis;
        [SerializeField] private float _battleDelay = 10f;
        
        
        private Round _round;
        private CombatOverlap _combatOverlap;
        private ExitingFromBattle _exitingFromBattle;
        
        private bool _isCanExecute;

        private bool _isBattleCompleted;
        private float _currentDelay;

        [Inject] public GameStateSwitcher GameStateSwitcher { get; set; }
        
        private void Start()
        {
            AntInject.Inject(this);

            _exitingFromBattle = new ExitingFromBattle(_playerCombatSystem, 16f);
            _round = new Round();
            _combatOverlap = new CombatOverlap(_triggerRadius, _playerCombatSystem.transform);
            AddHandlers();
            _combatOverlap.StartExecute();
            StopExecute();
        }

        private void OnDisable()
        {
            RemoveHandlers();
        }


        public void StartExecute()
        {
            _isCanExecute = true;
        }

        public void StopExecute()
        {
            _isCanExecute = false;
        }


        private void Update()
        {
            _combatOverlap.Execute();

            if (_isBattleCompleted)
            {
                _currentDelay -= Time.deltaTime;

                if (_currentDelay < 0)
                {
                    _combatOverlap.StartExecute();
                    _isBattleCompleted = false;
                }
            }
        }

        private void AddHandlers()
        {
            _combatOverlap.AddHandlers(TryAddGroup, BattleBegin);
            _playerCombatSystem.AddCombatActionCompletedHandlers(RoundEnd);
            _round.AddRoundEndHandlers(_playerCombatSystem.RoundEnd);
        }

        
        private void RemoveHandlers()
        {
            _combatOverlap.RemoveHandlers(TryAddGroup, BattleBegin);
            _playerCombatSystem.RemoveCombatActionCompletedHandlers(RoundEnd);
            _round.RemoveRoundEndHandlers(_playerCombatSystem.RoundEnd);
        }

        private async void BattleBegin()
        {
            _round.BattleBegin();
            StartExecute();
            GameStateSwitcher.SwitchGameOnTurnBase();
            
            DNVUI.Get<MainUI>().GetController<BattleController>().AddNextRoundHandlers(RoundEnd);
            
            
            await DNVUI.Get<MainUI>().GetController<FightController>().ShowFight();
            await DNVUI.Get<MainUI>().GetController<RoundController>().SetRound(_round.Value).ShowFight();
            
            DNVUI.Get<MainUI>().GetController<BattleController>().ShowNextRoundButton();
            
            _playerCombatSystem.BattleBegin();
            
            _exitingFromBattle.SetBattleBeginPosition(_playerCombatSystem.transform.position);
        }

        private async void RoundEnd()
        {
            if(!_isCanExecute) return;
            
            if (!_exitingFromBattle.CheckExitingTheBattle() && _enemyAis.Count > 0)
            {
                _round.RoundEnd();
                await DNVUI.Get<MainUI>().GetController<RoundController>().SetRound(_round.Value).ShowFight();
            }
            else
            {
                BattleCompleted();
            }
        }
        
        private async void BattleCompleted()
        {
            for (int i = 0; i < _enemyAis.Count; i++)
            {
                _enemyAis[i].BattleCompleted();
            }
            _enemyAis.Clear();
            GameStateSwitcher.SwitchGameOnRuntime();
            StopExecute();
            _playerCombatSystem.BattleCompleted();
            
            await DNVUI.Get<MainUI>().GetController<FightController>().CompleteBattle();
            
            DNVUI.Get<MainUI>().GetController<BattleController>().RemoveNextRoundHandlers(RoundEnd);
            DNVUI.Get<MainUI>().GetController<BattleController>().HideNextRoundButton();

            StartCalculateDelay();
        }

        private void StartCalculateDelay()
        {
            _isBattleCompleted = true;
            _currentDelay = _battleDelay;
        }


        private void TryAddGroup(List<EnemyAI> enemyAis)
        {
            foreach (var enemyAI in enemyAis)
            {
                if (_enemyAis.TryAdd(enemyAI))
                {
                    enemyAI.AddHealthOverHandler(RemoveEnemyAiFromGroup);
                    _round.AddRoundEndHandlers(enemyAI.RoundEnd);
                    enemyAI.BattleBegin();
                    _enemyAis.Add(enemyAI);
                }
            }
        }

        private void RemoveEnemyAiFromGroup(EnemyAI enemyAI)
        {
            if (!_enemyAis.TryAdd(enemyAI))
            {
                _enemyAis.Remove(enemyAI);
                enemyAI.BattleCompleted();
                _round.RemoveRoundEndHandlers(enemyAI.RoundEnd);
                enemyAI.RemoveHealthOverHandler(RemoveEnemyAiFromGroup);
            }

            Debug.Log(_enemyAis.Count);

           
        }
    }
}
