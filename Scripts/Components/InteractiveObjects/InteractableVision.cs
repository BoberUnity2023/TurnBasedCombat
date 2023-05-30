 using Components.OutlineEffect;
using QuickOutline;
using UnityEngine;

namespace Components.InteractiveObjects
{
    [RequireComponent(typeof(Outline))]
    public class InteractableVision : MonoBehaviour
    {
        [SerializeField] private OutlineData _outlineData;

        private Outline _outline;
        private OutlineSwitcher _outlineSwitcher;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _outline = GetComponent<Outline>();
            _outlineSwitcher = new OutlineSwitcher(_outlineData, _outline);
        }
        public void Enter()
        {
            _outlineSwitcher.OutlineOn();
        }
        public void Exit()
        {
            _outlineSwitcher.OutlineOff();
        }
        public void Use()
        {
            _outlineSwitcher.OutlineOff();
        }
    }
}