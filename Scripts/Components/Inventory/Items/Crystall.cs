using System.Collections.Generic;
using Components.Interactable;
using Components.Interactable.Interface;
using Components.InteractiveObjects;
using DNVMVC;
using Libraries.DNV.DNVPool.Interface;
using UnityEngine;

namespace Components.Inventory
{
    [RequireComponent(typeof(InteractableVision))]
    public class Crystall : MonoBehaviour, IInteractable, IPoolObject, ITelekinesable
    {
        [SerializeField] private ItemsBaseData _itemsData;
        [SerializeField] private string _itemId;        
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private List<Transform> _pointsToMove;

        private TargetPointsMovementInteractionObjects _targetPointsMovement;
        private HintWorldController _hintWorlsItemController;
        private InteractableVision _interactableVision;
        private Inventory _inventory;
        private Item _item;
        private IPool _pool;

        public Transform Owner => transform;
        public List<Transform> Points { get; }

        private void Start()
        {
            _targetPointsMovement = new TargetPointsMovementInteractionObjects(_pointsToMove);

            _interactableVision = GetComponent<InteractableVision>();
            _item = _itemsData.ItemById(_itemId);

            _hintWorlsItemController = Libraries.DNV.MVC.Core.DNVUI.Get<MainUI>().GetController<HintWorldController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                _inventory = inventory;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (ReferenceEquals(inventory, _inventory))
            {
                _inventory = null;
            }
        }

        public bool TryTake()
        {
            if (_inventory == null)
            {
                Debug.Log($"{gameObject.name} TryTake Wrong");
                return false;
            }

            Debug.Log($"{gameObject.name} TryTake Success!");
            _inventory.ItemAdd(_item);
            ReturnToPool();
            return true;
        }

       

        public void Enter()
        {
            Debug.Log($"{gameObject.name} Enter");
            _interactableVision.Enter();
        }

        public void Exit()
        {
            Debug.Log($"{gameObject.name} Exit");
            _interactableVision.Exit();
        }

        public void Use()
        {
            Debug.Log($"{gameObject.name} Use");
            _interactableVision.Use();
            TryTake();
        }

        public Transform FindNearPointToMove()
        {
            return _targetPointsMovement.FindNearPointToMove();
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            _pool.ReturnToPool(this);            
        }

        public void RegisterPool(IPool pool)
        {
            _pool = pool;
        }

        public void Extract()
        {
            transform.position = _startPosition;
            gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 position)
        {
            _startPosition = position;
            transform.position = position;
        }

        public void Throw()
        {
            gameObject.SetActive(false);
        }
    }
}

