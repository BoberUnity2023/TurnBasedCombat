namespace Stats.Effect.PositiveEffects
{
    public class FlockPositiveEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() + 10f;
        }
    }
}
