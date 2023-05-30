using Components.OutlineEffect;
using QuickOutline;

namespace Components.InteractiveObjects
{
    public class OutlineSwitcher
    {
        private readonly OutlineData _data;
        private readonly Outline _outline;

        public OutlineSwitcher(OutlineData data, Outline outline)
        {
            _data = data;
            _outline = outline;
            Init();
        }

        private void Init()
        {
            _outline.OutlineMode = _data.Mode;
            _outline.OutlineColor = _data.Color;
            _outline.OutlineWidth = _data.Width;
            OutlineOff();
        }

        public void OutlineOn()
        {
            _outline.enabled = true;
        }

        public void OutlineOff()
        {
            _outline.enabled = false;
        }
    }
}

