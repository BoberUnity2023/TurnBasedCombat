using Stats.Effect;
using Stats.Effect.PositiveEffects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "TestPassiveAbility", menuName = "Ability/Passive/TestPassiveAbility")]
    public class TestPassiveAbility : PassiveAbility
    {
        private SideStatProviderDecorator _sideStatProviderDecorator;
        
        protected override void Init()
        {
            _sideStatProviderDecorator = new HealthPointsPositiveEffect();
        }

        protected override void Cast()
        {
            SideStats.HealthPoints.AddEffect(_sideStatProviderDecorator);
            Debug.Log("added effect");
        }

        protected override bool IsCanCast()
        {
            return true;
        }

        protected override void ActionAfterAbilityCompleted()
        {
            SideStats.HealthPoints.RemoveEffect(_sideStatProviderDecorator);
        }

        protected override void ActionAfterRoundEnd()
        {
            Debug.Log("ActionAfterRoundEnd");
        }
    }
}