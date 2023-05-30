namespace DamageTypes
{
    public class FireDamageType : Damage, IMagicDamage
    {
        public FireDamageType(float minValue = 0, float maxValue = 0) : base(minValue, maxValue)
        {
        }

        public float Value => GetDamage();
       
    }
}