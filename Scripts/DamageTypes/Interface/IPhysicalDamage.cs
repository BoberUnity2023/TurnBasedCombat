namespace DamageTypes.Interface
{
    public interface IPhysicalDamage
    {
        public float Value { get; }
        
        public bool IsCriticalDamage { get; }

        public float CalculateDamage();
    }
    
}