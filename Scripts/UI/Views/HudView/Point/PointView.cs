using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class PointView
    {
        [SerializeField] private Image _magic;
        [SerializeField] private float _duration;

        public void Repaint(PointModel pointModel)
        {
            var remapHealth = (1f / pointModel.MaxPoint) * pointModel.Value;
            _magic.DOFillAmount(remapHealth, _duration).SetEase(Ease.Linear);
        }
    }
}