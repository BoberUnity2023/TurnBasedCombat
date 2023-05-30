using Components.Inventory;
using UnityEngine;
using Item = Components.Inventory.Item;

public class InventoryViewFiller: MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private ItemUI _itemUIPrefab;

    public void ReCreateListOfItems(Inventory inventory, InventoryView inventoryView)
    {
        ClearListOfItems();

        foreach (Item item in inventory.Items)
        {
            ItemUI itemUI = Instantiate(_itemUIPrefab, transform.position, Quaternion.identity);
            itemUI.InventoryView = inventoryView;
            itemUI.transform.SetParent(_content);
            itemUI.SetType(item.Type);
            itemUI.SetId(item.Id);
            itemUI.SetName(item.Name);
            itemUI.SetDescription(item.Description);
            itemUI.SetCount(item.Count);
            itemUI.SetIcon(item.Icon);
            itemUI.SetNew(item.IsNew);
        }
    }

    private void ClearListOfItems()
    {
        ItemUI[] inventoryItemUIs = _content.GetComponentsInChildren<ItemUI>();
        foreach (ItemUI inventoryItemUI in inventoryItemUIs)
        {
            Destroy(inventoryItemUI.gameObject);
        }
    }
}
