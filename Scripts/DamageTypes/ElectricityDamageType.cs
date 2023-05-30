namespace DamageTypes
{
    public class ElectricityDamageType : Damage, IMagicDamage
    {
        public ElectricityDamageType(float minValue = 0, float maxValue = 0) : base(minValue, maxValue)
        {
        }

        public float Value => GetDamage();
    }
}