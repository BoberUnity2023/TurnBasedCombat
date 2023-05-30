using System;
using Anthill.Utils;
using DNVMVC;
using UnityEngine;
using TMPro;

public class FpsView : UIElement, IView
{
    [SerializeField] private TMP_Text _count;
    private AntFpsCounter _fpsCounter = new AntFpsCounter();

    public void Show()
    {
        Activate = true;
    }

    public void Hide()
    {
        Activate = false;
    }

    private void Update()
    {
        if (!Activate) return;

        _fpsCounter.Update(Time.deltaTime);

        UpdateFpsCount(MathF.Round(_fpsCounter.Fps, 2));
    }

    public void UpdateFpsCount(float count)
    {
        _count.text = count.ToString("F0");
    }
}