using DNVMVC;

public class FpsController : IController
{
    private FpsView _view;
    
    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<FpsView>("UI/FpsView/FpsView", uiRoot.transform);        
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