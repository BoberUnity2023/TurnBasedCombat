using Stats.Policy.NormalPolicy;
using Stats.Side;
using Stats.SideStatsFactory;

namespace Stats
{
    public class SideStats
    {
        private Accuracy _accuracy;
        private ActionPoints _actionPoints;
        private CriticalStrikeChance _criticalStrikeChance;
        private EnergyShield _energyShield;
        private HealthPoints _healthPoints;
        private Initiative _initiative;
        
        private MagicalResistanceEffect _magicalResistanceEffect;
        
        
        
        
        private MagicPower _magicPower;
        private MoveActionPoints _moveActionPoints;
        
        
        private PhysicalResistanceEffect _physicalResistanceEffect;
        
        
        
        private ProtectionClass _protectionClass;
        private ProtectionFromCutting _protectionFromCutting;
        private ProtectionFromCrushing _protectionFromCrushing;
        private ProtectionFromEarth _protectionFromEarth;
        private ProtectionFromElectricity _protectionFromElectricity;
        private ProtectionFromFire _protectionFromFire;
        private ProtectionFromIce _protectionFromIce;
        private ProtectionFromStabbing _protectionFromStabbing;
        private SprintLimit _sprintLimit;
        private WeaponDamage _weaponDamage;
        private WeaponCost _weaponCost;

        public Accuracy Accuracy => _accuracy;
        public ActionPoints ActionPoints => _actionPoints;
        public CriticalStrikeChance CriticalStrikeChance => _criticalStrikeChance;
        public EnergyShield EnergyShield => _energyShield;
        public HealthPoints HealthPoints => _healthPoints;
        public Initiative Initiative => _initiative;
        public MagicalResistanceEffect MagicalResistanceEffect => _magicalResistanceEffect;
        public MagicPower MagicPower => _magicPower;
        public MoveActionPoints MoveActionPoints => _moveActionPoints;
        public PhysicalResistanceEffect PhysicalResistanceEffect => _physicalResistanceEffect;
        public ProtectionClass ProtectionClass => _protectionClass;
        public ProtectionFromCutting ProtectionFromCutting => _protectionFromCutting;
        public ProtectionFromEarth ProtectionFromEarth => _protectionFromEarth;
        public ProtectionFromElectricity ProtectionFromElectricity => _protectionFromElectricity;
        public ProtectionFromFire ProtectionFromFire => _protectionFromFire;
        public ProtectionFromIce ProtectionFromIce => _protectionFromIce;
        public ProtectionFromStabbing ProtectionFromStabbing => _protectionFromStabbing;
        public ProtectionFromCrushing ProtectionFromCrushing => _protectionFromCrushing;
        public SprintLimit SprintLimit => _sprintLimit;
        public WeaponDamage WeaponDamage => _weaponDamage;
        public WeaponCost WeaponCost => _weaponCost;

        public SideStats(UnitType unitType, BasicStats basicStats)
        {
            if (unitType == UnitType.Player)
            {
                CreateSideStatsForPlayer(basicStats);
            }
            else
            {
                CreateSideStatsForEnemy(basicStats);
            }
        }

        private void CreateSideStatsForPlayer(BasicStats stats)
        {
            _healthPoints = new HealthPoints(
                stats.Durability,
                stats.Level,
                new PlayerHealthPointFactory(),
                new PolicyThatStatsIsFilled(40),
                new PolicyThatStatsIsOver(0));

            _energyShield = new EnergyShield(
                stats.GenerationTurbine,
                stats.Level,
                new EnergyShieldStatValueFactory(),
                new PolicyThatStatsIsFilled(170),
                new PolicyThatStatsIsOver(0));

            _actionPoints = new ActionPoints(
                stats.Dexterity,
                stats.MechanicalHands,
                new PlayerActionPointsStatValueFactory(),
                new PolicyThatStatsIsFilled(16),
                new PolicyThatStatsIsOver(0));


            _criticalStrikeChance = new CriticalStrikeChance(
                stats.Intelligence,
                stats.Level,
                new CriticalStrikeChanceStatValueFactory(),
                new PolicyThatStatsIsFilled(12),
                new PolicyThatStatsIsOver(0));

            _accuracy = new Accuracy(
                stats.Dexterity,
                new AccuracyStatValueFactory(),
                new PolicyThatStatsIsFilled(145f),
                new PolicyThatStatsIsOver(95)
            );

            _initiative = new Initiative(
                stats.Level,
                new InitiativeStatValueFactory(),
                new PolicyThatStatsIsFilled(100),
                new PolicyThatStatsIsOver(11)
            );

            _magicalResistanceEffect = new MagicalResistanceEffect(
                stats.Intelligence,
                stats.Level,
                new PlayerMagicalResistanceDebuffsStatValueFactory(),
                new PolicyThatStatsIsFilled(78),
                new PolicyThatStatsIsOver(35)
            );

            _magicPower = new MagicPower(
                stats.Intelligence,
                stats.Level,
                new MagicPowerStatValueFactory(),
                new PolicyThatStatsIsFilled(35),
                new PolicyThatStatsIsOver(1)
            );

            _moveActionPoints = new MoveActionPoints(
                new MoveActionPointsStatValueFactory(),
                new PolicyThatStatsIsFilled(10),
                new PolicyThatStatsIsOver(0)
            );


            _physicalResistanceEffect = new PhysicalResistanceEffect(
                stats.Durability,
                stats.Level,
                new PlayerPhysicalResistanceDebuffsStatValueFactory(),
                new PolicyThatStatsIsFilled(100),
                new PolicyThatStatsIsOver(60)
            );

            _protectionClass = new ProtectionClass(
                stats.Durability,
                stats.Dexterity,
                new ProtectionClassStatValueFactory(),
                new PolicyThatStatsIsFilled(131),
                new PolicyThatStatsIsOver(65)
            );

            _sprintLimit = new SprintLimit(
                new SprintLimitStatValueFactory(),
                new PolicyThatStatsIsFilled(3),
                new PolicyThatStatsIsOver(0)
            );

            _weaponDamage = new WeaponDamage(
                stats.Strength,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(35),
                new PolicyThatStatsIsOver(1)
            );

            _weaponCost = new WeaponCost(0);
            _protectionFromCutting = new ProtectionFromCutting(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromEarth = new ProtectionFromEarth(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromElectricity = new ProtectionFromElectricity(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromFire = new ProtectionFromFire(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromIce = new ProtectionFromIce(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromStabbing = new ProtectionFromStabbing(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );

            _protectionFromCrushing = new ProtectionFromCrushing(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(20),
                new PolicyThatStatsIsOver(1)
            );
        }

        private void CreateSideStatsForEnemy(BasicStats stats)
        {
            _healthPoints = new HealthPoints(
                stats.Durability,
                stats.Level,
                new PlayerHealthPointFactory(),
                new PolicyThatStatsIsFilled(262),
                new PolicyThatStatsIsOver(0));

            _energyShield = new EnergyShield(
                stats.GenerationTurbine,
                stats.Level,
                new EnergyShieldStatValueFactory(),
                new PolicyThatStatsIsFilled(170),
                new PolicyThatStatsIsOver(0));

            _actionPoints = new ActionPoints(
                stats.Dexterity,
                stats.MechanicalHands,
                new EnemyActionPointsStatValueFactory(),
                new PolicyThatStatsIsFilled(16),
                new PolicyThatStatsIsOver(4));

            _criticalStrikeChance = new CriticalStrikeChance(
                stats.Intelligence,
                stats.Level,
                new CriticalStrikeChanceStatValueFactory(),
                new PolicyThatStatsIsFilled(17),
                new PolicyThatStatsIsOver(1));

            _accuracy = new Accuracy(
                stats.Dexterity,
                new AccuracyStatValueFactory(),
                new PolicyThatStatsIsFilled(170f),
                new PolicyThatStatsIsOver(95));

            _initiative = new Initiative(
                stats.Level,
                new InitiativeStatValueFactory(),
                new PolicyThatStatsIsFilled(60),
                new PolicyThatStatsIsOver(11));

            _magicalResistanceEffect = new MagicalResistanceEffect(
                stats.Intelligence,
                stats.Level,
                new PlayerMagicalResistanceDebuffsStatValueFactory(),
                new PolicyThatStatsIsFilled(60),
                new PolicyThatStatsIsOver(0));

            _magicPower = new MagicPower(
                stats.Intelligence,
                stats.Level,
                new MagicPowerStatValueFactory(),
                new PolicyThatStatsIsFilled(40),
                new PolicyThatStatsIsOver(1));

            _moveActionPoints = new MoveActionPoints(
                new MoveActionPointsStatValueFactory(),
                new PolicyThatStatsIsFilled(3),
                new PolicyThatStatsIsOver(0));

            _physicalResistanceEffect = new PhysicalResistanceEffect(
                stats.Durability,
                stats.Level,
                new PlayerPhysicalResistanceDebuffsStatValueFactory(),
                new PolicyThatStatsIsFilled(60),
                new PolicyThatStatsIsOver(0));

            _protectionClass = new ProtectionClass(
                stats.Durability,
                stats.Dexterity,
                new ProtectionClassStatValueFactory(),
                new PolicyThatStatsIsFilled(165),
                new PolicyThatStatsIsOver(65));

            _sprintLimit = new SprintLimit(
                new SprintLimitStatValueFactory(),
                new PolicyThatStatsIsFilled(3),
                new PolicyThatStatsIsOver(0));

            _weaponDamage = new WeaponDamage(
                stats.Strength,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(40),
                new PolicyThatStatsIsOver(1));

            _weaponCost = new WeaponCost(0);

            _protectionFromCutting = new ProtectionFromCutting(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );

            _protectionFromEarth = new ProtectionFromEarth(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );

            _protectionFromElectricity = new ProtectionFromElectricity(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );

            _protectionFromFire = new ProtectionFromFire(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );

            _protectionFromIce = new ProtectionFromIce(
                stats.Intelligence,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );

            _protectionFromStabbing = new ProtectionFromStabbing(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );
            
            
            _protectionFromCrushing = new ProtectionFromCrushing(
                stats.Durability,
                stats.Level,
                new WeaponDamageStatValueFactory(),
                new PolicyThatStatsIsFilled(30),
                new PolicyThatStatsIsOver(0)
            );
        }

        public void RoundEnd()
        {
            _actionPoints.Increment(_actionPoints.MaxValue);
            _moveActionPoints.Increment(_moveActionPoints.MaxValue);
        }

        public void BattleCompleted()
        {
            RoundEnd();
        }
    }

    public enum UnitType
    {
        Player,
        Enemy
    }
}