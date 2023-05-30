using Components.Player.Movement;
using DamageAcquisition;
using Stats;
using UnityEngine;

namespace Abilities.Active
{
    [CreateAssetMenu(fileName = "PlayerMoveAbility", menuName = "Ability/Active/PlayerMoveAbility")]
    public class PlayerMoveAbility : MoveAbility
    {
        private TurnBaseMovement _turnBaseMovement;

        public void Init(TurnBaseMovement turnBaseMovement)
        {
            _turnBaseMovement = turnBaseMovement;
        }

        public override void Move()
        {
            
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}