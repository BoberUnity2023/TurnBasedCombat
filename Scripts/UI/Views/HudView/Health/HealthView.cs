using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class HealthView
    {
        [SerializeField] private Image _shield;
        [SerializeField] private Image _hp;

        [SerializeField] private float _speed;

        public void Repaint(HealthModel healthModel)
        {
            var remapHealth = (1f / healthModel.MaxHealth) * healthModel.CurrentHealth;
            _hp.DOFillAmount(remapHealth, _speed).SetEase(Ease.Linear);
            
            var remapShield = (1f / healthModel.MaxShield) * healthModel.CurrentShield;
            _shield.DOFillAmount(remapShield, _speed).SetEase(Ease.Linear);
        }
    }
}