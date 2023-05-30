using DNVMVC;

public class PlayerStatsController : IController
{
    private PlayerStatsView _view;

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<PlayerStatsView>("UI/PlayerStats/PlayerStatsView", uiRoot.transform);
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
