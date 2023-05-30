using DNVMVC;

public class InventoryController : IController
{
    private InventoryView _view;

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<InventoryView>("UI/InventoryView/InventoryView", uiRoot.transform);
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
