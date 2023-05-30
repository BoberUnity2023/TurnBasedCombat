using DNVMVC.Views;
using UnityEngine;

namespace DNVMVC.Controllers
{
    public class LoaderScreenController : IController
    {
        private LoaderScreenView _loaderScreenView;

        public void CreateView(RootUI uiRoot)
        {
            _loaderScreenView =
                ObjectFactory.Create<LoaderScreenView>("UI/LoaderScreenView/ScreenLoader", uiRoot.transform);
        }

        public void Show()
        {
            _loaderScreenView.Show();
        }

        public void Hide()
        {
            _loaderScreenView.Hide();
        }
    }
}