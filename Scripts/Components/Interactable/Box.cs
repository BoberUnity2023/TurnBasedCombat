using System;
using System.Collections.Generic;
using Components.Interactable;
using Components.Interactable.Interface;
using Components.InteractiveObjects;
using DNVMVC;
using Libraries.DNV.MVC.Core;
using UnityEngine;

[RequireComponent(typeof(InteractableVision))]
public class Box : MonoBehaviour, IInteractable, ITelekinesable
{
    [SerializeField] private InteractableVision _interactableVision;
    [SerializeField] private List<Transform> _pointsToMove;

    private HintWorldController _hintWorldController;
    private TargetPointsMovementInteractionObjects _targetPointsMovement;

    public Transform Owner => transform;

    private void Start()
    {
        _targetPointsMovement = new TargetPointsMovementInteractionObjects(_pointsToMove);

        _hintWorldController = DNVUI.Get<MainUI>().GetController<HintWorldController>();        
    }

    public void Enter()
    {
        Debug.Log($"{gameObject.name} Enter");
        _interactableVision.Enter();
        _hintWorldController.HintShow(transform, "Box");
    }

    public void Exit()
    {
        Debug.Log($"{gameObject.name} Exit");
        _interactableVision.Exit();
        _hintWorldController.HintHide();
    }

    public void Use()
    {
        gameObject.SetActive(false);
        _interactableVision.Use();
    }

    public Transform FindNearPointToMove()
    {
        return _targetPointsMovement.FindNearPointToMove();
    }

    public void Throw()
    {
        gameObject.SetActive(false);
    }
}