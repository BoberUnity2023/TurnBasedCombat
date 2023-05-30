using System;
using System.Collections.Generic;
using UnityEngine;

namespace SystemUsingAbility.Interface
{
    public interface ISystemUsingAbility<T> where T : ScriptableObject
    {
        public TK ChangeCurrentAbility<TK>(TK ability) where TK : T;
        public TK ChangeCurrentAbility<TK>() where TK : T;
        public void CreateAbilities(List<T> original);
        public void AbilityCompletedHandler();
        public T GetAbility();
        public void AddAbilityCompleteHandler(Action abilityCompleted);
        public void RemoveAbilityCompleteHandler(Action abilityCompleted);

    }
}