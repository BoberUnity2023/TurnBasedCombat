using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DNVMVC.Views
{
    public class RoundView : UIElement, IView
    {
        [SerializeField] private TextMeshProUGUI _fightRound;
        [SerializeField] private TextMeshProUGUI _fightCount;

        [SerializeField] private RectTransform _pointMoveRight;
        [SerializeField] private RectTransform _pointMoveLeft;

        [SerializeField] private Ease _ease;

        private Tween _tween;

        private void Start()
        {
            _active = false;
            gameObject.SetActive(false);
        }

        public void RepaintCountRound(int value)
        {
            _fightCount.text = $"{value}";
        }

        public async Task ShowRound()
        {
            if (_tween != null && _tween.active) return;

            _active = true;
            gameObject.SetActive(true);

            _fightRound.rectTransform.DOMove(_pointMoveRight.position, 1).SetEase(_ease);
            _tween = _fightCount.rectTransform.DOMove(_pointMoveLeft.position, 1).SetEase(_ease);
            await _tween.AsyncWaitForKill();

            _fightRound.DOFade(0, 1);
            _tween = _fightCount.DOFade(0, 1);
            await _tween.AsyncWaitForKill();

            Hide();
        }
        
        public async void Show()
        {
            if (_tween != null && _tween.active) return;

            _active = true;
            gameObject.SetActive(true);

            _fightRound.rectTransform.DOMove(_pointMoveRight.position, 1).SetEase(_ease);
            _tween = _fightCount.rectTransform.DOMove(_pointMoveLeft.position, 1).SetEase(_ease);
            await _tween.AsyncWaitForKill();

            _fightRound.DOFade(0, 1);
            _tween = _fightCount.DOFade(0, 1);
            await _tween.AsyncWaitForKill();

            Hide();
        }

        public void Hide()
        {
            var color = _fightCount.color;
            color.a = 1;

            _fightRound.color = color;
            _fightCount.color = color;

            _fightRound.rectTransform.anchoredPosition = Vector3.zero;
            _fightCount.rectTransform.anchoredPosition = Vector3.zero;

            _active = false;
            gameObject.SetActive(false);
        }
    }
}