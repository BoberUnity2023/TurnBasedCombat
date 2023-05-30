using Anthill.Inject;
using Components.UI.HintWorldView;
using GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintWorld : MonoBehaviour, IViewEffect, ISwitchWhenGameStateChanges
{
    [SerializeField] private HintWorldViewData _data;
    private bool isShowing;

    private ViewEffect _viewEffect;

    public Vector2 Offset => _data.Offset;

    public GameObject Window => gameObject;

    [Inject] public GameState.GameStateSwitcher GameStateSwitcher { get; set; }

    public void Hide()
    {
        if (isShowing)
            _viewEffect.Hide();
        isShowing = false;
    }

    public void AfterHide()
    {        
        Destroy(gameObject);
    }

    private void Start()
    {
        AntInject.Inject(this);
        _viewEffect = new ViewEffect(this);
        _viewEffect.Show();
        GameStateSwitcher.TryAdd(this);
        isShowing = true;
    }

    public void SwitchOnTurnBase()
    {
        Hide();
    }

    public void SwitchOnRuntime()
    {
        
    }
}
