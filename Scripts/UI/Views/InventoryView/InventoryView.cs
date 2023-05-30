using Anthill.Inject;
using Components.Inventory;
using Components.Player;
using DNVMVC;
using UnityEngine;

[RequireComponent(typeof(InventoryViewFiller))]
[RequireComponent(typeof(HoverChecker))]

public class InventoryView : UIElement, IView, IViewEffect
{
    [SerializeField] private GameObject _window;
    [SerializeField] private DraggableItemUI _draggableSlot;    

    private Inventory _inventory;
    private ViewEffect _viewEffect;

    [Inject] public Player Player { get; set; }

    public GameObject Window => _window;

    public InventoryViewFiller Filler { get; set; }

    public HoverChecker CursorOnUIChecker { get; set; }

    public DraggableItemUI DraggableSlot 
    { 
        get => _draggableSlot; 
        set => _draggableSlot = value; 
    }    

    public void Start()
    {
        AntInject.Inject(this);
        _inventory = Player.GetComponent<Inventory>();
        Filler = GetComponent<InventoryViewFiller>();
        CursorOnUIChecker = GetComponent<HoverChecker>();
        _viewEffect = new ViewEffect(this);
    }

    public void Show()
    {
        Activate = true;       
    }

    public void Hide()
    {        
        Activate = false;
    }
    
    public void ResetNewState()
    {
        foreach (var item in _inventory.Items)
        {
            item.IsNew = false;
        }

        ItemUI[] itemUIs = GetComponentsInChildren<ItemUI>();
        foreach (ItemUI itemUI in itemUIs)
        {
            itemUI.SetNew(false);
        }        
    }

    public void PressOpen()
    {        
        _window.SetActive(true);
        SetOverOtherWindows();
        _viewEffect.Show();
        Filler.ReCreateListOfItems(_inventory, this);        
    }

    public void PressHide()
    {
        _viewEffect.Hide();        
    }

    public void AfterHide()
    {
        ResetNewState();        
    }
}