using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Abilities.Active;
using Anthill.Extensions;
using Stats;
using Stats.Side;
using SystemUsingAbility.Interface;
using Object = UnityEngine.Object;

namespace SystemUsingAbility
{
    public class SystemUsingActiveAbility : ISystemUsingAbility<ActiveAbility>
    {        
        private ActiveAbility _currentAbility;
        private Action _abilityCompleted;

        private List<ActiveAbility> _abilities;

        public ActiveAbility CurrentAbility => _currentAbility;
        public List<ActiveAbility> Abilities => _abilities;

        public SystemUsingActiveAbility(List<ActiveAbility> originAbilities, ActionPoints actionPoints, IOwnerSystemUsingAbility ownerSystemUsingAbility,Accuracy accuracy)
        {
            CreateAbilities(originAbilities);

            foreach (var activeAbility in _abilities)
            {
                activeAbility.Init(actionPoints, AbilityCompletedHandler, ownerSystemUsingAbility, accuracy);
            }
            
            ChangeCurrentAbility<IceRicochet>();
            ChangeCurrentAbility<TelekinesisItem>();            
            ChangeCurrentAbility<ViscousSplit>();
            ChangeCurrentAbility<EvilEye>();
            ChangeCurrentAbility<ArmoLight>();
            ChangeCurrentAbility<ArmoMiddle>();
            ChangeCurrentAbility<ArmoHard>();
            ChangeCurrentAbility<ArmoSuper>();
            ChangeCurrentAbility<MeteoriteStrike>();
        }

        public TK ChangeCurrentAbility<TK>(TK ability) where TK : ActiveAbility
        {
            _currentAbility = ability;// _abilities.FirstOrDefault(x => x is TK);
            return (TK)_currentAbility;
        }
        
        public TK ChangeCurrentAbility<TK>() where TK : ActiveAbility
        {
            _currentAbility = _abilities.FirstOrDefault(x => x is TK);
            return (TK)_currentAbility;
        }

        public void CreateAbilities(List<ActiveAbility> original)
        {
            _abilities = new List<ActiveAbility>();

            foreach (var activeAbility in original)
            {
                var ability = Object.Instantiate(activeAbility);
                if (_abilities.TryAdd(ability))
                {
                    _abilities.Add(ability);
                }
            }
        }

        public void AbilityCompletedHandler()
        {
            _abilityCompleted?.Invoke();
        }

        public ActiveAbility GetAbility()
        {
            return _currentAbility;
        }

        public void AddAbilityCompleteHandler(Action abilityCompleted)
        {
            _abilityCompleted += abilityCompleted;
        }
        
        public void RemoveAbilityCompleteHandler(Action abilityCompleted)
        {
            _abilityCompleted -= abilityCompleted;
        }

        public void RoundEnd()
        {
            foreach (var activeAbility in _abilities)
            {
                activeAbility.RoundEnd();
            }
        }
    }
}