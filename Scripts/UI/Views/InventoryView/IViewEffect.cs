using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewEffect
{
    public void AfterHide();

    public GameObject Window { get; }
}
