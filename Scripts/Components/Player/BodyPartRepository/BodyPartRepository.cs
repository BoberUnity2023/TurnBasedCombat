using Anthill.Inject;
using Components.WorldDissolve;
using UnityEngine;

namespace Components.Player.BodyPartRepository
{
    public class BodyPartRepository : MonoBehaviour
    {
        [SerializeField] private BodyPart _targetBodyPart;

        [Inject] public TransparentWorld TransparentWorld { get; set; }


        private void Start()
        {
            AntInject.Inject(this);
            TransparentWorld.Init(_targetBodyPart);
        }
    }
}