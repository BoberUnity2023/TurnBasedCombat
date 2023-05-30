using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Components.Player.Movement
{
    public class CharacterRotation
    {
        private readonly CharacterController _characterController;
        private readonly float _rotateSpeed;
        private Vector3 _direction;


        private bool _isCanExecute;

        private Tween _rotate;

        public CharacterRotation(CharacterController characterController, float rotateSpeed)
        {
            _characterController = characterController;
            _rotateSpeed = rotateSpeed;
            Start();
        }

        public void Start()
        {
            _rotate.Kill();
            _isCanExecute = true;
        }

        public void Stop()
        {
            _isCanExecute = false;
        }

        public void SetTargetPointToRotate(Vector3 targetPoint)
        {
            _direction = targetPoint - _characterController.transform.position;
            _direction.y = 0;
        }

        public void Execute()
        {
            if (!_isCanExecute) return;
          
            if (_direction == Vector3.zero) return;

            var eulerAngles = Quaternion.LookRotation(_direction, Vector3.up).eulerAngles;

            eulerAngles.x = 0;
            eulerAngles.z = 0;

            if (eulerAngles == Vector3.zero) return;

            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation,
                Quaternion.Euler(eulerAngles), _rotateSpeed * Time.deltaTime);
        }
        
        public Tween RotateToInteractableObject(Vector3 target)
        {
            _rotate.Kill();
            var direction = target - _characterController.transform.position;
            direction.y = 0;
            _direction = Vector3.zero;
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            _rotate = _characterController.transform.DORotate(lookRotation, 1f);
            _direction = Vector3.zero;
            return _rotate;
        }
    }
}