using DNVMVC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadView : UIElement, IView, IViewEffect
{
    [SerializeField] private GameObject _window;

    private ViewEffect _viewEffect;
    private bool isShown;

    public GameObject Window => _window;

    public void Start()
    {
        
    }    

    public void Show()
    {
        if (isShown)
            return;

        Activate = true;
        _window.SetActive(true);
        SetOverOtherWindows();
        _viewEffect = new ViewEffect(this);
        _viewEffect.Show();
        isShown = true;
    }

    public void Hide()
    {
        Activate = false;
    }

    public void PressExit()
    {
        Application.Quit();
    }

    public void PressRestart()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene("Main");
        _viewEffect.Hide();
    }

    public void AfterHide()
    {

    }
}
