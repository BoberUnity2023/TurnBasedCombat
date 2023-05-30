using System.Collections.Generic;
using UnityEngine;

namespace Components.Inventory
{
    [CreateAssetMenu(fileName = "ItemsBase", menuName = "Data/ItemsBase")]
    public class ItemsBaseData : ScriptableObject
    {
        [SerializeField] private List<Item> _items;

        public List<Item> Items => _items;

        public Item ItemById(string _id)
        {
            foreach(Item item in Items)
            {
                if (item.Id == _id)
                    return item;
            }

            Debug.LogWarning("Item Id: " + _id + " not founded");
            return null;
        }
    }
}