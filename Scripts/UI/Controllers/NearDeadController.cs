using Anthill.Inject;
using Components.Player;
using DNVMVC;

public class NearDeadController : IController
{
    private NearDeadView _view;
    
    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<NearDeadView>("UI/NearDeadView/NearDeadView", uiRoot.transform);
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
