using Components.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TMP_Text _indicatorName = null;
    [SerializeField] private TMP_Text _indicatorCount = null;
    [SerializeField] private TMP_Text _indicatorDescription = null;
    [SerializeField] private Image _iconImage = null;
    [SerializeField] private GameObject _indicatorNew = null;    

    private int _count = 0;

    public InventoryView InventoryView { get; set; }    

    public ItemType Type { get; set; }

    public string Name => _indicatorName.text;    

    public string Id { get; set; }

    public int Count
    {
        get => _count;
        private set
        {
            _count = value;
            SetCount(_count);
        }
    }

    public Sprite Icon
    {
        get => _iconImage.sprite; 
        private set
        {
            _iconImage.sprite = value;
            SetIcon(value);
        }
    }

    public void SetType(ItemType type)
    {
        Type = type; ;
    }

    public void SetId(string id)
    {
        Id = id;
    }

    public void SetName(string name)
    {
        _indicatorName.text = name;
    }

    public void SetDescription(string description)
    {
        _indicatorDescription.text = description;
    }

    public void SetCount(int count)
    {
        _count = count;
        _indicatorCount.text = count.ToString();
    }

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetNew(bool state)
    {         
        _indicatorNew.SetActive(state);
    }

    private void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {        
        InventoryView.DraggableSlot.OnItemUIPointEnter(this);
    }
}
