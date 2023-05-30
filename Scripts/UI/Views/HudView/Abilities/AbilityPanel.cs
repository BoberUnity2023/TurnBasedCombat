using Abilities;
using Anthill.Core;
using Anthill.Inject;
using Components.Player;
using SystemUsingAbility;
using UnityEngine;


public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private GameObject _buttonAbilityPrefab;
    private SystemUsingActiveAbility _systemUsingActiveAbility;

    [Inject] public Player Player { get; set; }

    private void Start()
    {
        AntInject.Inject(this);

        Init();
    }

    private void Init()
    {
        _systemUsingActiveAbility = Player.GetComponent<PlayerAbilitySystem>().SystemUsingActiveAbility;

        CreateButtonAbility(true);

        foreach (var ability in _systemUsingActiveAbility.Abilities)
        {
            CreateButtonAbility(ability);
        }
    }

    private ButtonAbility CreateButton
    {
        get
        {
            GameObject buttonAbilityObject = Instantiate(_buttonAbilityPrefab, transform.position, transform.rotation, transform);
            buttonAbilityObject.SetActive(true);
            return buttonAbilityObject.GetComponent<ButtonAbility>();
        }
    }

    private void CreateButtonAbility(ActiveAbility ability)
    {
        ButtonAbility buttonAbility = CreateButton;
        buttonAbility.Init(ability);
    }

    private void CreateButtonAbility(bool move)
    {
        ButtonAbility buttonAbility = CreateButton;
        buttonAbility.Move = move;        
    }
}
