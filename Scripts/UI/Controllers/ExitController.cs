using DNVMVC;

public class ExitController : IController
{
    private ExitView _view;

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<ExitView>("UI/ExitView/ExitView", uiRoot.transform);
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
