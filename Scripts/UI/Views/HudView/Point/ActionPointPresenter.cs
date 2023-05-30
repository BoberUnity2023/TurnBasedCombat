using UnityEngine;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class ActionPointPresenter
    {
        [SerializeField] private PointModel _pointModel;
        [SerializeField] private PointView _pointView;

        public void UpdateActionPointModel(PointModel healthModel)
        {
            _pointModel = healthModel;
        }

        public void Repaint()
        {
            _pointView.Repaint(_pointModel);
        }
    }
}