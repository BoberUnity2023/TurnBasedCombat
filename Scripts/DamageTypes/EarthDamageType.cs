namespace DamageTypes
{
    public class EarthDamageType : Damage, IMagicDamage
    {
        public EarthDamageType(float minValue = 0, float maxValue = 0) : base(minValue, maxValue)
        {
        }

        public float Value => GetDamage();
       
    }
}