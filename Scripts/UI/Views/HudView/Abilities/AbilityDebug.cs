using Anthill.Inject;
using Components.Player;
using SystemUsingAbility;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDebug : MonoBehaviour
{    
    private SystemUsingActiveAbility _systemUsingActiveAbility;
    private bool _isInited;

    [Inject] public Player Player { get; set; }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        AntInject.Inject(this);
        _systemUsingActiveAbility = Player.GetComponent<PlayerAbilitySystem>().SystemUsingActiveAbility;
        _isInited = true;
        OnValueChanged(false);
    }

    public void OnValueChanged(bool debug)
    {
        if (!_isInited)
            return;

        foreach (var ability in _systemUsingActiveAbility.Abilities)
        {
            ability.IsDebug = debug;
        }
    }
}
