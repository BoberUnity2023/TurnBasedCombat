using UnityEngine;
using DNVMVC;

namespace Components.UI.HintWorldView
{
    public class HintWorldView : UIElement, IView, IViewEffect    
    {
        [SerializeField] private GameObject _window;

        private HintWorld _hint;
        private Transform _owner;        

        private UnityEngine.Camera _camera;

        public IView CurrentView { get; set; }

        public GameObject Window => _window;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
            enabled = false;            
        }

        private void Update()
        {
            if (_hint == null)
                return;

            Vector3 position = _camera.WorldToScreenPoint(_owner.position) + new Vector3(_hint.Offset.x, _hint.Offset.y, 0);
            _hint.transform.SetPositionAndRotation(position, Quaternion.identity);
        }

        public void HintShow(Transform owner, string id)
        {            
            _owner = owner;
            _hint = ObjectFactory.Create<HintWorld>("UI/HintWorldView/Hint" + id, transform);            
            enabled = true;
        }

        public void HintHide()
        {
            _hint.Hide();            
            enabled = false;
        }

        public void Hide()
        {
            Activate = false;            
        }

        public void Show()
        {
            Activate = true;                   
        }  

        public void AfterHide()
        {
            
        }
    }
}

