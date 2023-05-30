using DNVMVC;
using UnityEngine;

public class ExitView : UIElement, IView, IViewEffect
{
    [SerializeField] private GameObject _window;
    private ViewEffect _viewEffect;

    public GameObject Window => _window;

    public void Start()
    {        
        _viewEffect = new ViewEffect(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _window.SetActive(true);
            SetOverOtherWindows();
            _viewEffect.Show();
        }
    }

    public void Show()
    {
        Activate = true;
    }

    public void Hide()
    {
        Activate = false;
    }

    public void PressYes()
    {
        Application.Quit();
    }

    public void PressNo()
    {
        _viewEffect.Hide();
    }

    public void AfterHide()
    {

    }
}
