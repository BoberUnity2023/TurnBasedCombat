using DNVMVC;
using UnityEngine;

namespace Components.Interactable
{
    public class ObjectActivator : MonoBehaviour
    {
        private IView _target;

        public ObjectActivator(string objectName)
        {
            GameObject o = GameObject.Find(objectName);
            
            if (o == null)
                o = GameObject.Find(objectName + "(Clone)");

            if (o == null)
            {
                Debug.LogWarning("Object " + objectName + " was not founded");
                return;
            }

            _target = o.GetComponent<IView>();
        }

        public void Activate()
        {
            _target.Show();
        }

        public void Deactivate()
        {
            _target.Hide();
        }
    }
}