using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Components.Inventory;
using Anthill.Inject;
using Components.Player;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class DraggableItemUI : Draggable, IPointerExitHandler
{
    [SerializeField] private InventoryView _inventoryView;

    private Inventory _inventory;    
    private Vector3 _startDragPosition;
    private RectTransform _rect;
    private Image _image;

    [Inject] public Player Player { get; set; }

    public DraggableSlotState State { get; set; }

    public Item ItemMemory { get; set; }

    private void Start()
    {
        AntInject.Inject(this);
        _inventory = Player.GetComponent<Inventory>();
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();  
        Hide();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging)
            return;

        Hide();
    }

    public void OnItemUIPointEnter(ItemUI itemUI)
    {
        if (State == DraggableSlotState.Load)
            return;

        PreloadItemDraggable(itemUI.Id);
        SetPosition(itemUI.transform.position);
        SetIcon(itemUI.Icon);        
        _image.raycastTarget = true;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        State = DraggableSlotState.Load;
        _startDragPosition = transform.position;
        Show();
        _image.raycastTarget = false;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);        
        SetPosition(_startDragPosition);
        Hide();
        if (ItemMemory == null)
            Debug.LogWarning("Item DragEnd OnItemDragEnd = null");

        HoverChecker hoverChecker = _inventoryView.CursorOnUIChecker;
        if (!hoverChecker.Hovered)
        {
            Debug.LogWarning("Item " + ItemMemory + " is out from Inventory");
            _inventory.ItemDrop(ItemMemory);            
            _inventoryView.Filler.ReCreateListOfItems(_inventory, _inventoryView);
        }
        ItemMemory = null;
        State = DraggableSlotState.Null;
    }

    private void PreloadItemDraggable(string id)
    {
        State = DraggableSlotState.Preload;
        Item item = _inventory.ItemById(id);
        ItemMemory = item;
    }

    private void SetIcon(Sprite sprite)
    {
        _image.sprite = sprite;        
    }

    private void SetPosition(Vector3 position)
    {        
        _rect.position = position;        
    }

    private void Hide()
    {
        _image.color = new Color(0, 0, 0, 0);             
    }

    private void Show()
    {
        _image.color = new Color(1, 1, 1, 1);        
    }
}
