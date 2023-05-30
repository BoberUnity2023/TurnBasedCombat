using Components.UI.HintWorldView;
using DNVMVC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillIndicatorWorldController : IController
{
    private UnitHintWorldView _view;

    private List<IView> _hints;

    public void CreateView(RootUI uiRoot)
    {
        _view = ObjectFactory.Create<UnitHintWorldView>("UI/HintWorldView/FillIndicatorWorldView", uiRoot.transform);        
        _view.Activate = false;

        //_hints = new List<IView>() { _view.Panel };        
    }

    public FillIndicatorWorldController SetViewByType<T>() where T : IView
    {
        _view.CurrentView = _hints.FirstOrDefault(x => x is T);

        return this;
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    public void SetValue(int value, int maxValue, string text)
    {
        _view.SetValue(value, maxValue, text);
    }
}
