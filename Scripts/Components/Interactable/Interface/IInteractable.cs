using System.Collections.Generic;
using UnityEngine;

namespace Components.Interactable.Interface
{
    public interface IInteractable
    {
        public Transform Owner { get; }
       
        public void Enter();
        public void Exit();
        public void Use();
        public Transform FindNearPointToMove();


    }
}