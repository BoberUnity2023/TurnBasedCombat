using System.Threading.Tasks;
using DNVMVC.Views;

namespace DNVMVC.Controllers
{
    public class FightController : IController
    {
        private FightView _fightView;

        public void CreateView(RootUI uiRoot)
        {
            _fightView = ObjectFactory.Create<FightView>("UI/FightView/FightView", uiRoot.transform);
        }

        public async void Show()
        {
            await _fightView.Show();
        }

        public async Task ShowFight()
        {
            await _fightView.Show();
        }
        
        public async Task CompleteBattle()
        {
            await _fightView.Complete();
        }

        public void Hide()
        {
            _fightView.Hide();
        }
    }
}