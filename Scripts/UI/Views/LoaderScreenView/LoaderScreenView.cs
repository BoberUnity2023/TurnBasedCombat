using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DNVMVC.Views
{
    public class LoaderScreenView : UIElement, IView
    {
        [SerializeField] private Image _screenLoader;

        [SerializeField] private Color _colrOnStartLoad;
        [SerializeField] private Color _colorOnFinishLoad;

        [SerializeField] private float _duration;

        public void Show()
        {
            Activate = true;

            _screenLoader.color = _colrOnStartLoad;
        }

        public async void Hide()
        {
            await _screenLoader.DOColor(_colorOnFinishLoad, _duration).SetEase(Ease.Linear).AsyncWaitForCompletion();

            Activate = false;
        }
    }
}