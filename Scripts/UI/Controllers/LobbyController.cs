using DNVMVC;
using Anthill.Utils;

public class LobbyController : IController
{
    private LobbyView _view;
    private AntFpsCounter _fpsCounter;
    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<LobbyView>("UI/LobbyView/LobbyView", uiRoot.transform);
        _view.Activate = false;
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }
}