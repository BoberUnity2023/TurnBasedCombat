using Anthill.Inject;
using Components.WorldTextEffect;
using Libraries.DNV.DNVPool.Pool;
using Libraries.DNV.DNVPool.PoolContainer;
using UnityEngine;

namespace Components.WorldText
{
    public class WorldTextVision
    {
        private Pool<PoolItems.WorldText> _monoPoolWorldText;        

        [Inject] public ObjectPoolContainer ObjectPoolContainer { get; set; }

        public WorldTextVision()
        {            
            AntInject.Inject(this);
            _monoPoolWorldText = ObjectPoolContainer.GetPool<PoolItems.WorldText>();
        }

        public PoolItems.WorldText Show(WorldTextType type, Vector3 position)
        {            
            PoolItems.WorldText worldText = _monoPoolWorldText.GetItem();
            worldText.SetType(type);
            worldText.SetPosition(position + Vector3.up * 2);

            return worldText;
        }        

        public void Show(WorldTextType type, Vector3 position, float value)
        {
            var worldText = Show(type, position);
            worldText.SetTextValue(value);
        }

        public void Show(WorldTextType type, Vector3 position, string text)
        {
            var worldText = Show(type, position);
            worldText.SetText(text);
        }
    }
}

