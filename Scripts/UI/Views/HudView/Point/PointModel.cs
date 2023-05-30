using Stats;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class PointModel
    {
        private readonly ActionPoints _actionPoints;
        public float MaxPoint => _actionPoints.MaxValue;
        public float Value => _actionPoints.Value;
        public PointModel(ActionPoints actionPoints)
        {
            _actionPoints = actionPoints;
        }
        
    }
}