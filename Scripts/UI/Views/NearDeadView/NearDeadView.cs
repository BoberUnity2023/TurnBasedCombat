using Anthill.Inject;
using Components.Player;
using DNVMVC;
using UnityEngine;

public class NearDeadView : UIElement, IView, IViewEffect
{
    [SerializeField] private GameObject _window;
    [SerializeField] private int _healthPoints;

    public GameObject Window => _window;

    [Inject] public Player Player { get; set; }


    private void Start()
    {
        AntInject.Inject(this);
        Player.GameStats.SideStats.HealthPoints.Change += HealthPointsChangedHandler;
    }

    private void OnDestroy()
    {
        AntInject.Inject(this);
        Player.GameStats.SideStats.HealthPoints.Change -= HealthPointsChangedHandler;
    }

    public void Show()
    {
        Activate = true;
    }

    public void Hide()
    {
        Activate = false;
    }


    public void AfterHide()
    {

    }

    private void HealthPointsChangedHandler()
    {
        if (Player.GameStats.SideStats.HealthPoints.Value < _healthPoints && !_window.activeSelf)
        {
            _window.SetActive(true);
        }

        if (Player.GameStats.SideStats.HealthPoints.Value > _healthPoints && _window.activeSelf ||
            Player.GameStats.SideStats.HealthPoints.Value <= 0 && _window.activeSelf)
        {
            _window.SetActive(false);
        }
    }
}
