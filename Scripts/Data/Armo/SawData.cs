using DamageTypes;
using DamageTypes.Interface;
using Resistance.Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "Saw", menuName = "Data/Armo/Saw")]
public class SawData : ArmoData
{
    private IPhysicalDamage _physicalDamage = new CuttingDamageType(0, 0, 0, 0);

    public IPhysicalDamage PhysicalDamage => _physicalDamage;
}
