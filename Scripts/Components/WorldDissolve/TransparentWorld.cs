using System;
using System.Collections.Generic;
using System.Linq;
using Anthill.Inject;
using Components.Camera.PlayerCamera;
using Extensions.Camera;
using UnityEngine;

namespace Components.WorldDissolve
{
    public class TransparentWorld : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private LayerMask _layerMask;

        private BodyPart _targetBodyPart;

        private bool _isInited;

        private List<ITransparency> _oldTransparency;



        public void Init(BodyPart bodyPart)
        {
            _targetBodyPart = bodyPart;
            _isInited = true;
            _oldTransparency = new List<ITransparency>();
        }


        private void FixedUpdate()
        {
            if (!_isInited) return;

            TryTransparentObject();
        }

        private void TryTransparentObject()
        {
            var ray = _camera.CastRayByScreenPosition(_targetBodyPart.Position);

            var distance = Vector3.Distance(_camera.gameObject.transform.position, _targetBodyPart.Position);
            
            var hits = Physics.RaycastAll(ray, distance, _layerMask);


            var newTransparent = new List<ITransparency>();
            
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out ITransparency transparency))
                {
                    if (TryAdded(transparency, newTransparent))
                    {
                        newTransparent.Add(transparency);
                    }
                }
            }

            if (hits.Length < 0)
            {
                for (int i = 0; i < newTransparent.Count; i++)
                {
                    newTransparent[i].ChangeMaterialByOpaque();
                }
            }
            else
            {
                for (int i = 0; i < newTransparent.Count; i++)
                {
                    newTransparent[i].ChangeMaterialByTransparent();
                }
            }

            if (newTransparent.Count > _oldTransparency.Count)
            {
                _oldTransparency = newTransparent;
            }
            else
            {
                for (int i = newTransparent.Count; i < _oldTransparency.Count; i++)
                {

                    Debug.Log($"{i} _oldTransparency[i].ChangeMaterialByOpaque();");
                    _oldTransparency[i].ChangeMaterialByOpaque();
                }

                _oldTransparency = newTransparent;
            }
            
        }
        

        private bool TryAdded(ITransparency transparency , List<ITransparency> transparencies)
        {
            if (transparencies.Contains(transparency))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        
    }

}