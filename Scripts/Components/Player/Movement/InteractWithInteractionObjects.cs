using Components.Interactable.Interface;
using DG.Tweening;
using UnityEngine;

namespace Components.Player.Movement
{
    public class InteractWithInteractionObjects
    {
        private readonly Transform _target;
        private readonly CharacterRotation _characterRotation;
        private IInteractable _interactable;

        private Tween _rotate;
        
        public InteractWithInteractionObjects(Transform target, CharacterRotation characterRotation)
        {
            _target = target;
            _characterRotation = characterRotation;
        }
        
        public void TryUse()
        {
            if (ReferenceEquals(_interactable, null))
            {
                return;
            }

            if (Vector3.Distance(_target.transform.position, _interactable.Owner.position) > 4f) return;
            
            _characterRotation.Stop();
            Use();
        }

        public void SetInteractableObject(IInteractable interactable)
        {
            _rotate.Kill();
            _interactable = interactable;
        }
        
        private void Use()
        {
            _rotate.Kill();
            _rotate = _characterRotation.RotateToInteractableObject(_interactable.Owner.position);
            _rotate.OnComplete(() => _interactable.Use());
        }
    }
}