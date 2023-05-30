using System;
using System.Collections.Generic;
using Anthill.Inject;
using Components.InteractiveObjects;
using Components.Player;
using Components.WorldText;
using Components.WorldTextEffect;
using DamageEffect;
using DamageTypes.Interface;
using Resistance;
using Resistance.Interface;
using Stats;
using UnityEngine;

namespace DamageAcquisition
{
    public class DamageAcquisitionSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] private InteractableVision _interactableVision;
        [SerializeField] private Player _player;

        private DamageResistanceRepository _damageResistance;
        private GameStats _gameStats;
        private WorldTextVision _worldTextVision;
        private List<AbilityEffect> _periodicDamages;

        public event Action<float> DamageTaken;
        private Action _died; 

        public Vector3 Position => transform.position;
        public SideStats SideStats => _gameStats.SideStats;

        public Transform Transform => transform;

        public void Init(GameStats gameStats)
        {
            AntInject.Inject(this);

            _gameStats = gameStats;
            _worldTextVision = new WorldTextVision();
            _damageResistance = new DamageResistanceRepository(_gameStats.SideStats);
            _periodicDamages = new List<AbilityEffect>();

            _gameStats.SideStats.HealthPoints.HealthOver += Dead;
        }

        public void RoundEnd()
        {
            for (int i = 0; i < _periodicDamages.Count; i++)
            {
                _periodicDamages[i].RoundEnd();
            }
        }

        public bool IsCanApplyPeriodicDamageEffect(AbilityEffect abilityEffect)
        {
            Debug.Log(_periodicDamages.Exists(x => x.GetType() == abilityEffect.GetType()));
            
            return !_periodicDamages.Exists(x => x.GetType() == abilityEffect.GetType());
        }

        public void TakeDamage(IMagicDamage damage)
        {
            var damageValue = damage.Value;
            if (!_gameStats.SideStats.EnergyShield.IsOver)
            {
                if (_damageResistance.TryGetResist(damage, out var magicResist))
                {
                    var newDamage = damageValue - magicResist.Value / 100f;
                    damageValue = newDamage;
                    _gameStats.SideStats.EnergyShield.Reduce(newDamage);
                }
                else
                {
                    _gameStats.SideStats.EnergyShield.Reduce(damage.Value);
                }
            }
            else
            {
                if (_damageResistance.TryGetResist(damage, out IMagicResist magicResist))
                {
                    var newDamage = damage.Value - magicResist.Value / 100f;
                    damageValue = newDamage;
                    _gameStats.SideStats.HealthPoints.Reduce(newDamage);
                }
                else
                {
                    _gameStats.SideStats.EnergyShield.Reduce(damage.Value);
                }
            }

            DamageTaken?.Invoke(damageValue);
            _worldTextVision.Show(WorldTextType.Blood, transform.position, damageValue);
        }

        public void TakeDamage(IPhysicalDamage damage)
        {
            var damageViewValue = damage.Value;
            if (!_gameStats.SideStats.EnergyShield.IsOver)
            {
                if (_damageResistance.TryGetResist(damage, out IPhysicalResist physicalResist))
                {
                    var newDamage = damage.Value - physicalResist.Value / 100f;
                    damageViewValue = newDamage;
                    _gameStats.SideStats.EnergyShield.Reduce(newDamage);
                }
                else
                {
                    _gameStats.SideStats.EnergyShield.Reduce(damage.Value);
                }
            }
            else
            {
                if (_damageResistance.TryGetResist(damage, out IPhysicalResist physicalResist))
                {
                    var newDamage = damage.Value - physicalResist.Value / 100f;
                    damageViewValue = newDamage;
                    _gameStats.SideStats.HealthPoints.Reduce(newDamage);
                }
                else
                {
                    _gameStats.SideStats.EnergyShield.Reduce(damage.Value);
                }
            }

            DamageTaken?.Invoke(damageViewValue);
            _worldTextVision.Show(WorldTextType.Blood, transform.position, damageViewValue);
            
            if (damage.IsCriticalDamage)
                _worldTextVision.Show(WorldTextType.CritacalDamage, transform.position);
        }

        public void AddPeriodicDamageEffect(AbilityEffect abilityEffect)
        {
            Debug.Log("AddPeriodicDamageEffect");
            
            _periodicDamages.Add(abilityEffect);
            abilityEffect.AddEffectCompletedHandlers(RemovePeriodicDamageEffectHandler);
        }

        private void RemovePeriodicDamageEffectHandler(AbilityEffect abilityEffect)
        {
            abilityEffect.RemoveEffectCompletedHandlers(RemovePeriodicDamageEffectHandler);
            Debug.Log(abilityEffect.GetType());
            _periodicDamages.Remove(abilityEffect);
        }

        public void Enter()
        {
            if (_interactableVision != null)
            {
                _interactableVision.Enter();
            }
        }

        public void Exit()
        {
            if (_interactableVision != null)
            {
                _interactableVision.Exit();
            }
        }

        public void AddDiedHandlers(Action diedHandler)
        {
            _died += diedHandler;
        }

        public void RemoveDiedHandlers(Action diedHandler)
        {
            _died -= diedHandler;
        }

        private void Dead()
        {
            Debug.Log(_periodicDamages.Count);
            
            for (int i = 0; i < _periodicDamages.Count; i++)
            {
                _periodicDamages[i].ReturnToPool();
                _periodicDamages[i].RemoveEffectCompletedHandlers(RemovePeriodicDamageEffectHandler);
            }

            _periodicDamages.Clear();
            _died?.Invoke();
        }
    }
}