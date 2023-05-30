using Stats.Basic;
using Stats.Policy.Interfaces;

namespace Stats
{
    public class BasicStats
    {
        private readonly Level _level;
        private readonly Strength _strength;
        private readonly Dexterity _dexterity;
        private readonly Durability _durability;
        private readonly Intelligence _intelligence;
        private readonly Luck _luck;
        private readonly MechanicalHands _mechanicalHands;
        private readonly GenerationTurbine _generationTurbine;
       
        /// <summary>
        /// Уровень
        /// </summary>
        public Level Level => _level;
        /// <summary>
        /// Сила
        /// </summary>
        public Strength Strength => _strength;
        /// <summary>
        /// Ловкость
        /// </summary>
        public Dexterity Dexterity => _dexterity;
        /// <summary>
        /// Стойкость
        /// </summary>
        public Durability Durability => _durability;
        /// <summary>
        /// Разум
        /// </summary>
        public Intelligence Intelligence => _intelligence;
        /// <summary>
        /// Удача
        /// </summary>
        public Luck Luck => _luck;
        /// <summary>
        /// Механические руки 
        /// </summary>
        public MechanicalHands MechanicalHands => _mechanicalHands;
        /// <summary>
        /// Генератор силового поля 
        /// </summary>
        public GenerationTurbine GenerationTurbine => _generationTurbine;
        
        
        
        public BasicStats(IPolicyThatStatsIsFilled normalFilledPolicy) //ToDO генерировать значение в зависимости от стартовых  +3 что выбрал пользователь
        {
            _level = new Level(1 , normalFilledPolicy);
            _strength = new Strength(5,  normalFilledPolicy);
            _dexterity = new Dexterity(5, normalFilledPolicy);
            _durability = new Durability(5, normalFilledPolicy);
            _intelligence = new Intelligence(5, normalFilledPolicy);
            _luck = new Luck(5, normalFilledPolicy);
            _mechanicalHands = new MechanicalHands(0);
            _generationTurbine = new GenerationTurbine(0);
        }
    }
}