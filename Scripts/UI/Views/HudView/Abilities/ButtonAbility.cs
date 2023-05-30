using Abilities;
using Anthill.Core;
using Anthill.Inject;
using Components.Player;
using SystemUsingAbility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAbility : MonoBehaviour
{
    [SerializeField] private GameObject _selectBorder;
    [SerializeField] private Image _acceleratorFill;
    [SerializeField] private TMP_Text _indicatorCooldown;
    [SerializeField] private TMP_Text _indicatorAccelerator;
    [SerializeField] private Color _acceleratorColorNormal;
    [SerializeField] private Color _acceleratorColorReady;
    private SystemUsingActiveAbility _systemUsingActiveAbility;
    private PlayerAbilitySystem _playerAbilitySystem;

    private ActiveAbility _ability;
    private Image _image;    

    public bool Move { get; set; }
    [Inject] public Player Player { get; set; }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        AntInject.Inject(this);        

        AntDelayed.Call(Time.fixedDeltaTime, () =>
        {
            _systemUsingActiveAbility = Player.GetComponent<PlayerAbilitySystem>().SystemUsingActiveAbility;
            _playerAbilitySystem = Player.GetComponent<PlayerAbilitySystem>();
            SetSelected(Move);
            //_selectBorder.SetActive(false/*_systemUsingActiveAbility.CurrentAbility == _ability*/);
        });        
    }

    private void Update()
    {
        //_button.interactable = Ability.IsAvialable;        

        UpdateAcseleration();
        UpdateCooldownIndicator();
        UpdateSelectBorder();
    }

    public void Init(ActiveAbility ability)
    {        
        _ability = ability;        
        _image.sprite = _ability.Icon;

        GameObject acselerator = _acceleratorFill.transform.parent.gameObject;
        acselerator.SetActive(_ability.UseAccelerator);        
    }

    public void Press()
    {
        ButtonAbility[] abilityButtons = transform.parent.GetComponentsInChildren<ButtonAbility>();
        foreach (var abilityButton in abilityButtons)
        {
            abilityButton.SetSelected(false);
        }
        SetSelected(true);

        if (Move)
        {
            _playerAbilitySystem.ChangeActiveAbilityOnMoveAbility();
        }
        else
        {
            _playerAbilitySystem.ChangeMoveAbilityOnActiveAbility();
            _playerAbilitySystem.ChangeCurrentAbility(_ability);  
        }
    }  
    
    public void SetSelected(bool state)
    {
        _selectBorder.SetActive(state);
    }

    private void UpdateAcseleration()
    {
        if (!Move && _ability.UseAccelerator)
        {
            _indicatorAccelerator.text = _ability.Accelerator.ToString() + " %";
            _acceleratorFill.fillAmount = (float)_ability.Accelerator / 100;
            _acceleratorFill.color = _ability.Accelerator == 100 ? _acceleratorColorReady : _acceleratorColorNormal;
        }
    }

    private void UpdateCooldownIndicator()
    {
        string text = "";
        //if (Ability.CooldownCurrent > 0 && Ability.CooldownCurrent < 90)
        //    text = Ability.CooldownCurrent.ToString();

        _indicatorCooldown.text = text;
    }

    private void UpdateSelectBorder()
    {
        //_selectBorder.SetActive(Ability.Selected);
    }
}
