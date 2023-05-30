using Anthill.Inject;
using Components.InteractablePicker;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _click;
    [Inject] public PositionPicker PositionPicker { get; set; }
    
    public void EnableParticleClick()
    {
        _click.gameObject.SetActive(true);
    }

    public void DisableParticleClick()
    {
        _click.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        AntInject.Inject(this);
        PositionPicker.AddRayFindCallback(MoveToPosition);
    }
    
    private void OnDisable()
    {
        PositionPicker.RemoveRayFindCallback(MoveToPosition);
    }

    private void MoveToPosition(Vector3 position)
    {
        _click.transform.position = position + Vector3.up / 2;
        _click.Play();
    }
}