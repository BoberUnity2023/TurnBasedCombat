using DamageTypes.Interface;
using Resistance.Interface;

namespace DamageTypes
{
    public class CrushingDamageType : Damage, IPhysicalDamage
    {
        private readonly IPhysicalDamage _criticalDamageEffect;
      
        public float Value => _criticalDamageEffect.Value;
        public bool IsCriticalDamage => _criticalDamageEffect.IsCriticalDamage;


        public CrushingDamageType(float minValue, float maxValue, int criticalDamageChance = 0, int criticalDamageModificator = 0) : base(minValue, maxValue)
        {
            _criticalDamageEffect = new CriticalEffect(this, criticalDamageChance, criticalDamageModificator);
        }
        
        public float CalculateDamage()
        {
            return GetDamage();
        }

     
    }
    
    
}