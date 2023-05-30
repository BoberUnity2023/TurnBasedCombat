using Components.UI.ChangeHpViewData;
using DNVMVC;
using TMPro;
using UnityEngine;

public class ChangeHpView : UIElement, IView
{    
    [SerializeField] private ChangeHpViewData _data;
    [SerializeField] private TMP_Text _indicator;

    public Vector2 Offset => _data.Offset;

    public void Init(int value)
    {  
        _indicator.text = Text(value);
        _indicator.color = Color(value);
        Destroy(gameObject, 3);
    }    

    public void Hide()
    {
        
    }

    public void Show()
    {
        
    }

    private string Text(int value)
    {
        if (value < 0)        
            return value.ToString();

        return "+" + value.ToString();
    }

    private Color Color(int value)
    {
        if (value < 0)        
            return _data.ColorRemove; 

        return _data.ColorAdd;        
    }
}
