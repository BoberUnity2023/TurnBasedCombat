using UnityEngine;
using DamageAcquisition;
using Abilities;
using System.Collections.Generic;

namespace SystemUsingAbility.Interface
{
    public interface IOwnerSystemUsingAbility
    {
        public Vector3 Position { get; }

        public IDamageable Damageable { get; }

        public List<PassiveAbility> PassiveAbilities { get; set; }
    }
}