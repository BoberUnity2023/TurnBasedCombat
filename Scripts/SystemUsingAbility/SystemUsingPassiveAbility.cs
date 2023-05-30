using System.Collections.Generic;
using Abilities;
using Anthill.Extensions;
using Stats;
using SystemUsingAbility.Interface;
using UnityEngine;

namespace SystemUsingAbility
{
    public class SystemUsingPassiveAbility
    {
        private List<PassiveAbility> _passiveAbilities;

        public SystemUsingPassiveAbility(List<PassiveAbility> originAbilities, IOwnerSystemUsingAbility ownerSystemUsingAbility, SideStats sideStats)
        {
            CreateAbilities(originAbilities, ownerSystemUsingAbility, sideStats);
        }
        
        private void CreateAbilities(List<PassiveAbility> original, IOwnerSystemUsingAbility ownerSystemUsingAbility, SideStats sideStats)
        {
            _passiveAbilities = new List<PassiveAbility>();

            foreach (var activeAbility in original)
            {
                var ability = Object.Instantiate(activeAbility);
                
                if (_passiveAbilities.TryAdd(ability))
                {
                    ability.Init(ownerSystemUsingAbility, sideStats);
                    _passiveAbilities.Add(ability);
                }
            }
        }

        
        public void RoundEnd()
        {
            foreach (var passiveAbility in _passiveAbilities)
            {
                passiveAbility.TryCast();
            }
            
            foreach (var activeAbility in _passiveAbilities)
            {
                activeAbility.RoundEnd();
            }
        }

        public void BattleCompleted()
        {
            foreach (var passiveAbility in _passiveAbilities)
            {
                passiveAbility.Complete();
            }
        }
    }
}