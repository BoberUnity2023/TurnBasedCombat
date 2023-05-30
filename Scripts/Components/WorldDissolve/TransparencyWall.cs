using System;
using Anthill.Utils;
using DG.Tweening;
using UnityEngine;

namespace Components.WorldDissolve
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TransparencyWall : MonoBehaviour, ITransparency
    {
        private Renderer _renderer;
        private MaterialPropertyBlock _propertyBlock;
        
        private float _targetTransparentValue;
        private float _currentTransparentValue;

        private bool _isNeedChangeValue;

        private float _minTransparentValue = 0.33f;
        private float _maxTransparentValue = 1f;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void ChangeMaterialByTransparent()
        {
            _currentTransparentValue = _minTransparentValue;
            ChangePropertyBlock();
        }

        public void ChangeMaterialByOpaque()
        {
            _currentTransparentValue = _maxTransparentValue;
            
            ChangePropertyBlock();
        }

        private void ChangePropertyBlock()
        {
            _propertyBlock.SetFloat("_EffectOpacity" , _currentTransparentValue);
            _renderer.SetPropertyBlock(_propertyBlock);      
        }
    }
    
}