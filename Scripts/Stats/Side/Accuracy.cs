using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.Accuracy;
using Stats.SideStatsProvider.HealthPoint;

namespace Stats.Side
{
    public class Accuracy 
    {
        private float _value;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;

        private ISideStatProvider _sideStatProvider;
        

        public float Value => _value;

        public Accuracy(IBasicStats basicStats, SideStatsValueFactory valueFactory, IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _basicStats = basicStats;
            _sideStatProvider = new AccuracyProvider(basicStats, valueFactory);
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
    }
}