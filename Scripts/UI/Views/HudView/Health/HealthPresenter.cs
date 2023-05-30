using UnityEngine;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class HealthPresenter
    {
        [SerializeField] private HealthModel _healthModel;
        [SerializeField] private HealthView _healthView;

        public void UpdateHealthModel(HealthModel healthModel)
        {
            _healthModel = healthModel;
        }
        public void Repaint()
        {
            _healthView.Repaint(_healthModel);
        }
    }
}