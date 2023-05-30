namespace Stats.Effect.PositiveEffects
{
    public class FlockPositiveEffectX2 : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() + 20f;
        }
    }
}
