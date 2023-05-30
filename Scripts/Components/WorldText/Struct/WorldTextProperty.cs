using System;
using Components.WorldTextEffect;
using DamageNumbersPro;
using UnityEngine;

namespace Components.WorldText.Struct
{
    [Serializable]
    public struct WorldTextProperty
    {
        [SerializeField] private WorldTextType _type;
        [SerializeField] private DamageNumber _prefab;
        [SerializeField] private float _lifeTime;

        public WorldTextType Type => _type;
        public DamageNumber Prefab => _prefab;
        public float LifeTime => _lifeTime;
    }
}