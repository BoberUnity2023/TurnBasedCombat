using System;
using UnityEngine;

namespace Components.Inventory
{   
    [Serializable]
    public class Item
    {
        [SerializeField] private string _id;
        [SerializeField] private ItemType _type;
        [SerializeField] private string _name;
        [SerializeField] private int _count;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon = null;
        private bool _isNew = false;

        public string Id => _id;

        public ItemType Type => _type;

        public string Name => _name;

        public int Count { get => _count; set => _count = value; }

        public string Description => _description;

        public Sprite Icon => _icon;

        public bool IsNew { get => _isNew; set => _isNew = value; }

        public Item(Item item)
        {
            _id = item.Id;
            _type = item.Type;
            _name = item.Name;
            _count = item.Count;
            _description = item.Description;
            _icon = item.Icon;
            _isNew = item.IsNew;
        }
    }
}