using UnityEngine;
using DNVMVC;
using Components.UI.HintWorldView;
using Components.Interactable.Interface;
using Components.Interactable;
using Libraries.DNV.MVC.Core;

public class Unit : MonoBehaviour, IInteractable
{
    [SerializeField] private HintWorldViewData _hintData;    

    private FillIndicatorWorldController _controller;
    private ObjectActivator _objectActivator;

    public Transform Owner => transform;

    private void Start()
    {
        _controller = DNVUI.Get<MainUI>().GetController<FillIndicatorWorldController>();
        _controller.Show();        
        
        int _hp = Random.Range(0, 140);
        //_controller.SetViewByType<PanelFillIndicatorWorldUnitView>().SetValue(_hp, 140, "HP");

        _objectActivator = new ObjectActivator("WindowEnemy");
    }

    public void Enter()
    {
        Debug.Log($"{gameObject.name} Enter");        
        _objectActivator.Activate();
    }

    public void Exit()
    {
        Debug.Log($"{gameObject.name} Exit");
        _objectActivator.Deactivate();
    }

    public void Use()
    {
        Debug.Log($"{gameObject.name} Use");
        int _hp = Random.Range(0, 140);
        //_controller.SetViewByType<PanelFillIndicatorWorldUnitView>().SetValue(_hp, 140, "HP");
    }

    public Transform FindNearPointToMove()
    {
        return transform;
    }
}
