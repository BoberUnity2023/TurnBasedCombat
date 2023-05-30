using DG.Tweening;
using UnityEngine;

public class ViewEffect
{    
    private Transform _viewTransform;
    private CanvasGroup _viewCanvasGroup;
    private IViewEffect _view;

    private float _time = 0.5f;

    public ViewEffect (IViewEffect view)
    {
        Init(view);
    } 

    public void Init(IViewEffect view)
    {
        _view = view;
        _viewTransform = view.Window.transform;
        _viewCanvasGroup = view.Window.GetComponent<CanvasGroup>();        
    }

    public void Show()
    {
        _viewTransform.localScale = Vector3.zero;
        _viewCanvasGroup.alpha = 0;

        _viewTransform.DOScale(Vector3.one, _time).SetEase(Ease.OutBack);
        _viewCanvasGroup.DOFade(1, _time);
    }

    public async void Hide()
    {
        _viewCanvasGroup.DOFade(0, _time);
        await _viewTransform.DOScale(Vector3.zero, _time).SetEase(Ease.InSine).AsyncWaitForCompletion();
        _view.AfterHide();
    }
}
