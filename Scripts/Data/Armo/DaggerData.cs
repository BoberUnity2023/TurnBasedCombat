using DamageTypes;
using DamageTypes.Interface;
using Resistance.Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Dagger", menuName = "Data/Armo/Dagger")]
public class DaggerData : ArmoData
{
    private IPhysicalDamage _physicalDamage = new StabbingDamageType(0, 0, 0, 0);

    public IPhysicalDamage PhysicalDamage => _physicalDamage;
}
