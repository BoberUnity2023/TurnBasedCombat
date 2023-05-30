using Stats.Side;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class HealthModel
    {
        private readonly HealthPoints _healthPoints;
        private readonly EnergyShield _energyShield;
        public float MaxHealth => _healthPoints.MaxValue;
        public float MaxShield => _energyShield.MaxValue;
        public float CurrentHealth => _healthPoints.Value;
        public float CurrentShield => _energyShield.Value;

        public HealthModel(HealthPoints healthPoints, EnergyShield energyShield)
        {
            _healthPoints = healthPoints;
            _energyShield = energyShield;
        }
    }
}