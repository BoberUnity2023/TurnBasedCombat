using System;
using DNVMVC.Views;

namespace DNVMVC.Controllers
{
    public class BattleController : IController
    {
        private BattleView _battleView;

        private Action _nextRound;
        
        public void CreateView(RootUI uiRoot)
        {
            _battleView = ObjectFactory.Create<BattleView>("UI/BattleView/BattleView", uiRoot.transform);
        }

        public BattleController Init()
        {
            _battleView.Init();
            return this;
        }

        public void Show()
        {
           _battleView.Show();
           AddHandlers();
        }

        public void Hide()
        {
            _battleView.Hide();
            RemoveHandlers();
        }

        private void AddHandlers()
        {
            _battleView.NextRoundView.Clicked += NextRoundHandler;
        }

        private void RemoveHandlers()
        {
            _battleView.NextRoundView.Clicked -= NextRoundHandler;
        }

        private void NextRoundHandler()
        {
            _nextRound?.Invoke();
        }

        public void AddNextRoundHandlers(Action callback)
        {
            _nextRound += callback;
        }

        public void RemoveNextRoundHandlers(Action callback)
        {
            _nextRound -= callback;
        }

        public void ShowNextRoundButton()
        {
            _battleView.NextRoundView.Show();
        }
        
        public void HideNextRoundButton()
        {
            _battleView.NextRoundView.Hide();
        }
    }
}