using UnityEngine;

public abstract class ArmoData : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _attackPrice;    
    [SerializeField] private int _criticalDamageChance;
    [SerializeField] private int _criticalDamageModificator;
    [SerializeField] private int _attackDistance;
    [SerializeField] private AttackClass _attackClass;
    [SerializeField] private int _effectivenessDamageFromArmo;

    public string Title => _title;

    public int MinDamage => _minDamage;

    public int MaxDamage => _maxDamage;

    public int AttackPrice => _attackPrice;
    
    public int CriticalDamageChance => _criticalDamageChance;

    public int CriticalDamageModificator => _criticalDamageModificator;

    public int AttackDistance => _attackDistance;

    public AttackClass AttackClass => _attackClass;

    public int EffectivenessDamageFromArmo => _effectivenessDamageFromArmo;
}
