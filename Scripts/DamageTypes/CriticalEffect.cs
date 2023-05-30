using DamageTypes.Interface;

namespace DamageTypes
{
    public class CriticalEffect : IPhysicalDamage
    {
        private readonly IPhysicalDamage _damage;
        private readonly CriticalDamageEffectFactory _criticalDamageEffectFactory;
        private readonly int _criticalDamageChance;
        private readonly int _criticalDamageModificator;

        private float _criticalDamage;
        private bool _isCriticalDamage;
        public float Value => CalculateDamage();

        public bool IsCriticalDamage => _isCriticalDamage;
        
        public CriticalEffect(IPhysicalDamage damage, int criticalDamageChance, int criticalDamageModificator)
        {
            _damage = damage;
            _criticalDamageEffectFactory = new CriticalDamageEffectFactory();
            _criticalDamageChance = criticalDamageChance;
            _criticalDamageModificator = criticalDamageModificator;
        }
       
        public float CalculateDamage()
        {
            return _criticalDamageEffectFactory.TryApplyCriticalDamage(_damage.CalculateDamage(), _criticalDamageChance,
                _criticalDamageModificator, out _isCriticalDamage);
        }
        
    }
}