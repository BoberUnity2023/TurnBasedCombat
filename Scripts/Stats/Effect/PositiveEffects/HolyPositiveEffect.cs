namespace Stats.Effect.PositiveEffects
{
    internal class HolyPositiveEffect : SideStatProviderDecorator
    {
        public override float Calculate()
        {
            return _sideStatProvider.Calculate() + 5f;
        }
    }
}