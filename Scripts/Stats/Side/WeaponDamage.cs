using Items;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Policy.Interfaces;
using Stats.Policy.NormalPolicy;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider.HealthPoint;

namespace Stats.Side
{
    public class WeaponDamage : IUpdateStats
    {
        private float _value;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private ISideStatProvider _sideStatProvider;
        public float Value => _value;
        public WeaponDamage(IBasicStats basicStats, Level level, SideStatsValueFactory valueFactory,
            IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _level = level;
            _sideStatProvider = new HealthPointsProvider(basicStats, level, valueFactory);
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

        public void Calculate()
        {
            _value = _sideStatProvider.Calculate();
        }

        public void UpdateStats(int min, int max)
        {
            _policyThatStatsIsOver = new PolicyThatStatsIsOver(min);
            _policyThatStatsIsFilled = new PolicyThatStatsIsFilled(max);
            Calculate();
        }
    }
}