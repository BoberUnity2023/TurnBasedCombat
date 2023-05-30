using DNVMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNVMVC
{
    public class LobbyView : UIElement, IView
    {
        public void Hide()
        {
            Activate = false;
        }

        public void Show()
        {
            Activate = true;
        }
    }
}
