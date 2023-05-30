using UnityEngine;

namespace Components.WorldDissolve
{
    public class BodyPart : MonoBehaviour
    {
        public bool IsVisible;
        public Vector3 Position => transform.position;
    }
}