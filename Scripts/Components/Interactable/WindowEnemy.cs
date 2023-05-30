using DNVMVC;
using TMPro;
using UnityEngine;

public class WindowEnemy : MonoBehaviour, IView
{
    [SerializeField] private GameObject _window;
    [SerializeField] private TMP_Text _labelName;
    [SerializeField] private TMP_Text _labelHp;
    [SerializeField] private TMP_Text _labelInitiative;


    public void SetName(string text)
    {
        _labelName.text = text;
    }

    public void SetHp(int value)
    {
        _labelHp.text = value.ToString();
    }

    public void SetInitiative(int value)
    {
        _labelHp.text = value.ToString();
    }

    public void Show()
    {
        _window.SetActive(true);
    }

    public void Hide()
    {
        _window.SetActive(false);
    }
}
