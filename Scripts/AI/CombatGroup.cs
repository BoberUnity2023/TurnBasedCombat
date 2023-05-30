using System.Collections.Generic;
using Anthill.Extensions;
using UnityEngine;

namespace AI
{
    public class CombatGroup : MonoBehaviour
    {
        private List<EnemyAI> _enemyAis = new List<EnemyAI>();

        public List<EnemyAI> EnemyAis => _enemyAis;

        public void AddItemInGroup(EnemyAI ai)
        {
            if (_enemyAis.TryAdd(ai))
            {
                _enemyAis.Add(ai);
            }
        }

        public void RemoveItemFromGroup(EnemyAI ai)
        {
            if (!_enemyAis.TryAdd(ai))
            {
                _enemyAis.Remove(ai);
            }
        }
        
    }
}