using Components.Interactable.Interface;
using Components.InteractiveObjects;
using Components.Containers.States;
using UnityEngine;
using System.Collections.Generic;

namespace Components.Containers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(InteractableVision))]
    public class Container : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableVision _vision;
        [SerializeField] private Transform _player;
        [SerializeField] private float _distanceOpen;
        [SerializeField] private List<Item> _items;

        
        private ContainerBehaviour _containerBehaviour;           

        public Transform Owner => transform;
        public List<Transform> Points { get; }
        public Animator Animator { get; private set; }

        public List<Item> Items { get; set; }

        public bool HasItems => Items.Count > 0;        

        public void Init()
        {            
            _containerBehaviour = new ContainerBehaviour(this);
            Animator = GetComponent<Animator>();
            Items = _items;
        }

        private void Start()
        {
            Init();
        }

        public void ChangeType<T>() where T : BaseContainerState
        {
            _containerBehaviour.SwitchState<T>();
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
            _containerBehaviour.TryOpen();
        }
    }
}
