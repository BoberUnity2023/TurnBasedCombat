using UnityEngine;
using TMPro;
using DNVMVC;
using UnityEngine.UI;
using Anthill.Inject;
using Components.Player;

public class PlayerStatsView : UIElement, IView, IViewEffect
{
    [Inject] public Player Player { get; set; }

    [SerializeField] private GameObject _window;
    [SerializeField] private Button _buttonShow;
    
    [SerializeField] private TMP_Text _indicatorLevel;
    [SerializeField] private TMP_Text _indicatorStrength;
    [SerializeField] private TMP_Text _indicatorDexterity;
    [SerializeField] private TMP_Text _indicatorDurability;
    [SerializeField] private TMP_Text _indicatorIntelligence;
    [SerializeField] private TMP_Text _indicatorLuck;

    [SerializeField] private TMP_Text _indicatorHP;
    [SerializeField] private TMP_Text _indicatorEnergyShield;
    [SerializeField] private TMP_Text _indicatorActionPoints;
    [SerializeField] private TMP_Text _indicatorMoveActionPoints;
    [SerializeField] private TMP_Text _indicatorSprintLimit;
    
    [SerializeField] private TMP_Text _indicatorWeaponDamage;    
    [SerializeField] private TMP_Text _indicatorAccuracy;
    [SerializeField] private TMP_Text _indicatorMagicPower;
    [SerializeField] private TMP_Text _indicatorCriticalStrikeChance;
    [SerializeField] private TMP_Text _indicatorInitiative;

    [SerializeField] private TMP_Text _indicatorProtectionClass;
    [SerializeField] private TMP_Text _indicatorPhysicalResistanceEffect;
    [SerializeField] private TMP_Text _indicatorMagicalResistanceEffect;
    [SerializeField] private TMP_Text _indicatorProtectionFromStabbing;
    [SerializeField] private TMP_Text _indicatorProtectionFromCutting;

    [SerializeField] private TMP_Text _indicatorProtectionFromCrushing;
    [SerializeField] private TMP_Text _indicatorProtectionFromFire;
    [SerializeField] private TMP_Text _indicatorProtectionFromIce;
    [SerializeField] private TMP_Text _indicatorProtectionFromElectricity;
    [SerializeField] private TMP_Text _indicatorProtectionFromEarth;

    private ViewEffect _viewEffect;

    public GameObject Window => _window;    

    private void Start()
    {
        AntInject.Inject(this);
        _viewEffect = new ViewEffect(this);        
    }

    public void ShowInfo()
    {
        _indicatorLevel.text = Player.GameStats.BasicStats.Level.Value.ToString();
        _indicatorStrength.text = Player.GameStats.BasicStats.Strength.Value.ToString();
        _indicatorDexterity.text = Player.GameStats.BasicStats.Dexterity.Value.ToString();
        _indicatorDurability.text = Player.GameStats.BasicStats.Durability.Value.ToString();
        _indicatorIntelligence.text = Player.GameStats.BasicStats.Intelligence.Value.ToString();
        _indicatorLuck.text = Player.GameStats.BasicStats.Luck.Value.ToString();

        _indicatorHP.text = Player.GameStats.SideStats.HealthPoints.Value.ToString("f1");
        _indicatorEnergyShield.text = Player.GameStats.SideStats.EnergyShield.Value.ToString("f1");
        _indicatorActionPoints.text = Player.GameStats.SideStats.ActionPoints.Value.ToString("f1");
        _indicatorMoveActionPoints.text = Player.GameStats.SideStats.MoveActionPoints.Value.ToString("f1");
        _indicatorSprintLimit.text = Player.GameStats.SideStats.SprintLimit.Value.ToString("f1");

        _indicatorWeaponDamage.text = Player.GameStats.SideStats.WeaponDamage.Value.ToString("f1");
        _indicatorAccuracy.text = Player.GameStats.SideStats.Accuracy.Value.ToString("f1") + " %";
        _indicatorMagicPower.text = Player.GameStats.SideStats.MagicPower.Value.ToString("f1");
        _indicatorCriticalStrikeChance.text = Player.GameStats.SideStats.CriticalStrikeChance.Value.ToString("f1") + " %";
        _indicatorInitiative.text = Player.GameStats.SideStats.Initiative.Value.ToString("f1");

        _indicatorProtectionClass.text = Player.GameStats.SideStats.ProtectionClass.Value.ToString("f1") + " %";
        _indicatorPhysicalResistanceEffect.text = Player.GameStats.SideStats.PhysicalResistanceEffect.Value.ToString("f1") + " %";
        _indicatorMagicalResistanceEffect.text = Player.GameStats.SideStats.MagicalResistanceEffect.Value.ToString("f1") + " %";
        _indicatorProtectionFromStabbing.text = Player.GameStats.SideStats.ProtectionFromStabbing.Value.ToString("f1");
        _indicatorProtectionFromCutting.text = Player.GameStats.SideStats.ProtectionFromCutting.Value.ToString("f1");

        _indicatorProtectionFromCrushing.text = Player.GameStats.SideStats.ProtectionFromCrushing.Value.ToString("f1");
        _indicatorProtectionFromFire.text = Player.GameStats.SideStats.ProtectionFromFire.Value.ToString("f1");
        _indicatorProtectionFromIce.text = Player.GameStats.SideStats.ProtectionFromIce.Value.ToString("f1");
        _indicatorProtectionFromElectricity.text = Player.GameStats.SideStats.ProtectionFromElectricity.Value.ToString("f1");
        _indicatorProtectionFromEarth.text = Player.GameStats.SideStats.ProtectionFromEarth.Value.ToString("f1");
    }

    public void PressShow()
    {
        _buttonShow.gameObject.SetActive(false);
        SetOverOtherWindows();
        _window.transform.localScale = Vector3.zero;
        _viewEffect.Show();
        _window.SetActive(true);
        ShowInfo();
    }

    public void PressHide()
    {
        _viewEffect.Hide();        
    }

    public void AfterHide()
    {
        _window.SetActive(false);
        _buttonShow.gameObject.SetActive(true);
    }

    public void Show()
    {
        Activate = true;
    }

    public void Hide()
    {
        Activate = false;
    }
}
