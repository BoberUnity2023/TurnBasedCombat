using Components.UI.HintWorldView;
using DNVMVC;
using UnityEngine;

public class HintWorldController : IController
{    
    private HintWorldView _view;    

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<HintWorldView>("UI/HintWorldView/WorldHintView", uiRoot.transform);        
        _view.Activate = false;        
    }  
    
    public void HintShow(Transform owner, string id)
    {        
        _view.HintShow(owner, id);
    }
    public void HintHide()
    {
        _view.HintHide();
    }

    public void Show()
    {        
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }
}
