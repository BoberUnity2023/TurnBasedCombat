using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DNVMVC.Views
{
    public class FightView : UIElement
    {
        [SerializeField] private TextMeshProUGUI _fightBegin;
        [SerializeField] private TextMeshProUGUI _fightBattle;
        [SerializeField] private TextMeshProUGUI _fightCompleted;
        
        
        [SerializeField] private RectTransform _pointMoveRight;
        [SerializeField] private RectTransform _pointMoveLeft;

        [SerializeField] private Ease _ease;

        private Vector3 _startPositionFightBattle;

        private Vector3 _startPositionFightCompleted;
        
        private Vector3 _startPositionFightBegin;
        

        private Tween _tween;

        private void Start()
        {
            Activate = false;
            _startPositionFightBattle = _fightBattle.rectTransform.position;
            _startPositionFightBegin = _fightBegin.rectTransform.position;
            _startPositionFightCompleted = _fightCompleted.rectTransform.position;
        }

        public async Task Show()
        {
            if (_tween != null && _tween.active) return;

            Activate = true;
            _fightCompleted.gameObject.SetActive(false);
            _fightBegin.gameObject.SetActive(true);
            
            _fightBegin.rectTransform.DOMove(_pointMoveRight.position, 1).SetEase(_ease);
            _tween = _fightBattle.rectTransform.DOMove(_pointMoveLeft.position, 1).SetEase(_ease);
            await _tween.AsyncWaitForKill();

            _fightBegin.DOFade(0, 1);
            _tween = _fightBattle.DOFade(0, 1);
            await _tween.AsyncWaitForKill();

            Hide();
        }
        
        public async Task Complete()
        {
            if (_tween != null && _tween.active) return;

            Activate = true;

            _fightCompleted.gameObject.SetActive(true);
            _fightBegin.gameObject.SetActive(false);
            
            _fightCompleted.rectTransform.DOMove(_pointMoveRight.position, 1).SetEase(_ease);
            _tween = _fightBattle.rectTransform.DOMove(_pointMoveLeft.position, 1).SetEase(_ease);
            await _tween.AsyncWaitForKill();

            _fightCompleted.DOFade(0, 3);
            _tween = _fightBattle.DOFade(0, 3);
            await _tween.AsyncWaitForKill();

            Hide();
        }

        public void Hide()
        {
            var color = _fightBattle.color;
            color.a = 1;

            _fightBattle.color = color;
            _fightBegin.color = color;
            _fightCompleted.color = color;

            _fightBegin.rectTransform.position = _startPositionFightBegin;
            _fightBattle.rectTransform.position = _startPositionFightBattle;
            _fightCompleted.rectTransform.position = _startPositionFightCompleted;

            Activate = false;
        }
    }
}