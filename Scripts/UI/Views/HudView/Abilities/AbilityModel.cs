using Abilities;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class AbilityModel
    {
        public ActiveAbility Ability => _ability;
        // public int ActionPointPrice => _ability.ActionPointPrice;
        // public int CooldownRoundsCount => _ability.CooldownRoundsCount;
        // public int NumberInitiatives => _ability.NumberInitiatives;

        private readonly ActiveAbility _ability;
        public AbilityModel(ActiveAbility ability)
        {
            _ability = ability;
        }
        
        
    }
}