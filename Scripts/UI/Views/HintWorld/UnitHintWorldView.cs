using DNVMVC;
using UnityEngine;

namespace Components.UI.HintWorldView
{
    public class UnitHintWorldView : UIElement, IView
    {        

        public IView CurrentView { get; set; }
        

        public void Hide()
        {
            Activate = false;
            CurrentView.Hide();
        }

        public void Show()
        {
            Activate = true;
            CurrentView.Show();
        }

        public void SetValue(int value, int maxValue, string text)
        {
            //Panel.SetValue(value, maxValue, text);
        }
    }
}