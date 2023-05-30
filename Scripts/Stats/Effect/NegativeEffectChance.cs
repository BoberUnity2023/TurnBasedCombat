using DamageAcquisition;
using Stats;
using UnityEngine;

public class NegativeEffectChance
{
    public bool TryAddDebuff(float baseChance, SideStats sideStats, NegativeEffectType type)
    {        
        float resistanceEffect = 0;        

        switch (type)
        {
            case NegativeEffectType.Physical:
                resistanceEffect = sideStats.PhysicalResistanceEffect.Value;
                break;

            case NegativeEffectType.Magical:
                resistanceEffect = sideStats.MagicalResistanceEffect.Value;
                break;  
        }

        float chance = baseChance * (1 - resistanceEffect / 100);

        return Random.Range(0, 100) < chance;
    }
}
