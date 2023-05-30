using DNVMVC;

public class DeadController : IController
{
    private DeadView _view;

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<DeadView>("UI/DeadView/DeadView", uiRoot.transform);
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
