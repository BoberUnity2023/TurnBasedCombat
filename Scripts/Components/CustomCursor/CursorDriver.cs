using UnityEngine;
using Components.CustomCursor;

public class CursorDriver : MonoBehaviour
{
    [SerializeField] private GameCursor _customCursor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _customCursor.SetCursorState(CursorState.Pressed);

        if (Input.GetMouseButtonUp(0))
            _customCursor.SetCursorState(CursorState.Default);
    }

    public void OnInteractiveObjectEnter()
    {
        _customCursor.SetCursorState(CursorState.Active);
    }

    public void OnInteractiveObjectExit()
    {
        _customCursor.SetCursorState(CursorState.Default);
    }

    public void OnInteractiveObjectUse()
    {
        _customCursor.SetCursorState(CursorState.Default);
    }
}