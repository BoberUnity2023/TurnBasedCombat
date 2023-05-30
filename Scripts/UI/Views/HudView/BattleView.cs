using Anthill.Inject;
using Combat;
using Components.Player;
using UI.Views.HudView.NextRoundView;
using UnityEngine;

namespace DNVMVC.Views
{
    public class BattleView : UIElement, IView
    {
        [SerializeField] private HealthPresenter _healthPresenter;
        [SerializeField] private ActionPointPresenter _actionPointPresenter;

        [SerializeField] private AbilityPresenter _instanceAbilityPresentor;
        [SerializeField] private Transform _parentAbilityView;

        [SerializeField] private NextRoundView _nextRoundView;

        private PlayerCombatSystem _playerCombatSystem;

        public NextRoundView NextRoundView => _nextRoundView;

        [Inject] public Player Player { get; set; }

        public void Init()
        {
            AntInject.Inject(this);

            _healthPresenter.UpdateHealthModel(new HealthModel(Player.GameStats.SideStats.HealthPoints,
                Player.GameStats.SideStats.EnergyShield));
            _actionPointPresenter.UpdateActionPointModel(new PointModel(Player.GameStats.SideStats.ActionPoints));

            _actionPointPresenter.Repaint();
            _healthPresenter.Repaint();

            Player.GameStats.SideStats.HealthPoints.Change += _healthPresenter.Repaint;
            Player.GameStats.SideStats.EnergyShield.Change += _healthPresenter.Repaint;

            Player.GameStats.SideStats.ActionPoints.Change += _actionPointPresenter.Repaint;

            _playerCombatSystem = Player.GetComponent<PlayerCombatSystem>();
            _playerCombatSystem.BattleBeginEvent += Show;
            _playerCombatSystem.BattleCompletedEvent += Hide;
            Hide();
        }

        private void OnDestroy()
        {
            Player.GameStats.SideStats.HealthPoints.Change -= _healthPresenter.Repaint;
            Player.GameStats.SideStats.EnergyShield.Change -= _healthPresenter.Repaint;

            Player.GameStats.SideStats.ActionPoints.Change -= _actionPointPresenter.Repaint;

            _playerCombatSystem.BattleBeginEvent -= Show;
            _playerCombatSystem.BattleCompletedEvent -= Hide;
        }

        public void Show()
        {            
            Activate = true;
        }

        public void Hide()
        {            
            Activate = false; 
        }
    }
}