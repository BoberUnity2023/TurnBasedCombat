using System.Threading.Tasks;
using DNVMVC.Views;
using UnityEngine;

namespace DNVMVC.Controllers
{
    public class RoundController : IController
    {
        private RoundView _roundView;

        public void CreateView(RootUI uiRoot)
        {
            _roundView = ObjectFactory.Create<RoundView>("UI/RondView/RondView", uiRoot.transform);
        }

        public RoundController SetRound(int value)
        {
            _roundView.RepaintCountRound(value);
            return this;
        }

        public async Task ShowFight()
        {
            await _roundView.ShowRound();
        }

        public void Show()
        {
            _roundView.Show();
        }

        public void Hide()
        {
            _roundView.Hide();
        }
    }
}