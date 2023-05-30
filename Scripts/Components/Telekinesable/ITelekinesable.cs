using UnityEngine;

public interface ITelekinesable
{
    public void Throw();

    public Transform Owner { get; }

    public void Enter() { }

    public void Exit() { }
}
