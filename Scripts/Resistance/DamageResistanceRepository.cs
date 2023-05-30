using System.Collections.Generic;
using System.Linq;
using DamageTypes;
using DamageTypes.Interface;
using Resistance.Interface;
using Stats;

namespace Resistance
{
    public class DamageResistanceRepository
    {
        private readonly List<MyDictionaryData<IPhysicalDamage, IPhysicalResist>> _physicalResists;
        private readonly List<MyDictionaryData<IMagicDamage, IMagicResist>> _magicalResists;

        public DamageResistanceRepository(SideStats sideStats)
        {
            _physicalResists = new List<MyDictionaryData<IPhysicalDamage, IPhysicalResist>>()
            {
                new MyDictionaryData<IPhysicalDamage, IPhysicalResist>(new CrushingDamageType(0,0), sideStats.ProtectionFromCrushing),
                new MyDictionaryData<IPhysicalDamage, IPhysicalResist>(new CuttingDamageType(0,0), sideStats.ProtectionFromCutting),
                new MyDictionaryData<IPhysicalDamage, IPhysicalResist>(new StabbingDamageType(0,0), sideStats.ProtectionFromStabbing),
                new MyDictionaryData<IPhysicalDamage, IPhysicalResist>(new BleedingDamageType(0,0,0,
                    0), sideStats.ProtectionClass)
               
            };

            _magicalResists = new List<MyDictionaryData<IMagicDamage, IMagicResist>>()
            {
                new MyDictionaryData<IMagicDamage, IMagicResist>(new FireDamageType(0,0), sideStats.ProtectionFromFire),
                new MyDictionaryData<IMagicDamage, IMagicResist>(new IceDamageType(0,0), sideStats.ProtectionFromIce),
                new MyDictionaryData<IMagicDamage, IMagicResist>(new EarthDamageType(0,0), sideStats.ProtectionFromEarth),
                new MyDictionaryData<IMagicDamage, IMagicResist>(new ElectricityDamageType(0,0), sideStats.ProtectionFromElectricity),
            };
        }

        public bool TryGetResist(IMagicDamage magicDamage, out IMagicResist magicResist)
        {
            var resist = _magicalResists.FirstOrDefault(x => x.Item1.GetType() == magicDamage.GetType())?.Item2;

            if (ReferenceEquals(resist, null))
            {
                magicResist = null;
                return false;

            }
            else
            {
                magicResist = resist;
                return true;
            }
        }
        
        
        public bool TryGetResist(IPhysicalDamage magicDamage, out IPhysicalResist physicalResist)
        {
            var resist = _physicalResists.FirstOrDefault(x => x.Item1.GetType() == magicDamage.GetType())?.Item2;

            if (ReferenceEquals(resist, null))
            {
                physicalResist = null;
                return false;

            }
            else
            {
                physicalResist = resist;
                return true;
            }
            
        }
    }
}