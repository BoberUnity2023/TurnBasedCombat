using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemsBaseData _itemsData;

        public List<Item> Items { get; private set; }

        public event Action<Item, bool> ItemAdded;
        public event Action<Item> ItemRemoved;
        public event Action<Item> ItemDropped;
        public event Action<Item> ItemUsed;

        private void Awake()        
        {
            Items = new List<Item>();
        }        

        public bool TryItemUse(Item item)
        {
            if (!HasItem(item.Id))            
                return false;            

            Debug.Log("Inventory. Used item: " + item.Name);
            ItemUsed?.Invoke(item);
            ItemRemove(item);
            return true;
        }

        public void ItemAdd(Item newItem)
        {
            Item item = ItemById(newItem.Id);
            bool hasTheSameItem = item != null;

            if (hasTheSameItem)
            {
                item.Count += newItem.Count;
                Debug.Log("Inventory. Added existing item: " + item.Name + " " + item.Count);
            }
            else
            {
                Item tempItem = new Item(newItem);                
                tempItem.IsNew = true;
                Items.Add(tempItem);
                Debug.Log("Inventory. Added new item: " + newItem.Name + " " + newItem.Count);
            }
            ItemAdded?.Invoke(newItem, hasTheSameItem);
        }

        public void ItemRemove(Item item)
        {
            Debug.Log("Inventory. Removed item: " + item.Name);
            if (item.Count > 1)
                item.Count--;
            else
                Items.Remove(item);

            ItemRemoved?.Invoke(item);
        }

        public void ItemDrop(Item item)
        {
            ItemDropped?.Invoke(item);
            ItemRemove(item);
        }

        public Item ItemById(string itemId)
        {
            foreach (var item in Items)
            {
                if (item.Id == itemId)
                    return item;
            }

            return null;
        }

        public bool HasItem(string itemId)
        {
            foreach(Item item in Items)
            {
                if (item.Name == itemId)
                    return true;
            }

            return false;
        }
    }
}