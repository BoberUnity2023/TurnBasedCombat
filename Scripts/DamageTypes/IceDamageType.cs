namespace DamageTypes
{
    public class IceDamageType : Damage, IMagicDamage
    {
        public IceDamageType(float minValue = 0, float maxValue = 0) : base(minValue, maxValue)
        {
        }

        public float Value => GetDamage();
       
    }
}