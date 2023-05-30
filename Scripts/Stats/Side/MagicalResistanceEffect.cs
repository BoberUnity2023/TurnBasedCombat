using Resistance.Interface;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.HealthPoint;
using Stats.SideStatsProvider.MagicalResistanceDebuffs;

namespace Stats.Side
{
    public class MagicalResistanceEffect : IMagicResist
    {
        private float _value;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private ISideStatProvider _sideStatProvider;

        public float Value => _value / 100f;

        public MagicalResistanceEffect(IBasicStats basicStats, Level level, SideStatsValueFactory valueFactory, IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _level = level;
            _sideStatProvider = new MagicalResistanceDebuffsProvider(basicStats, level, valueFactory);
            _policyThatStatsIsOver = policyThatStatsIsOver;
            _policyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }

        public void AddHandlers()
        {
            _basicStats.ValueChanged += Calculate;
            _level.ValueChanged += Calculate;
        }

        public void RemoveHandlers()
        {
            _basicStats.ValueChanged -= Calculate;
            _level.ValueChanged -= Calculate;
        }

        private void Calculate()
        {
            _value = _sideStatProvider.Calculate();
        }
    }
}