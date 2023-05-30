using System.Collections.Generic;
using Components.Interactable.Interface;
using Components.InteractiveObjects;
using Components.OutlineEffect;
using Components.Door.States;
using UnityEngine;

namespace Components.Door
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableVision _visionMain;
        
        [SerializeField] private Transform _player;

        [SerializeField] private float _distanceOpen;

        private InteractableVision _vision;
        private Vector3 _targetPosition;

        public Vector3 TargetPosition => _targetPosition;

        public Transform Owner => transform;
        public List<Transform> Points { get; }

        public Animator Animator { get; private set; }

        public DoorBehaviour DoorBehaviour { get; private set; }

        public void Init()
        {
            DoorBehaviour = new DoorBehaviour(this);
            Animator = GetComponent<Animator>();
            _targetPosition = transform.position;
        }

        private void Start()
        {
            Init();
        }

        public void ChangeDoorType<T>() where T : BaseDoorState
        {
            DoorBehaviour.SwitchState<T>();
        }


        public void Enter()
        {
            Debug.Log($"{gameObject.name} Enter");
            _vision.Enter();
        }

        public void Exit()
        {
            Debug.Log($"{gameObject.name} Exit");
            _vision.Exit();
        }

        public void Use()
        {
            Debug.Log($"{gameObject.name} Use");

            float distToPlayer = Vector3.Distance(transform.position, _player.position);
            if (distToPlayer < _distanceOpen)
            {
                UseStart();
            }
            else
            {
                Debug.Log($"{gameObject.name} Player move to door. Dist: " + distToPlayer);                
            }
        }

        public Transform FindNearPointToMove()
        {
            return transform;
        }

        public void PlayerCameUpToMe()
        {
            UseStart();
        }

        public void UseStart()
        {
            DoorBehaviour.TryOpen();             
        }
    }
}


