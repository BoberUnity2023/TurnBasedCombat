using Resistance.Interface;
using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.Policy.Interfaces;
using Stats.SideStatsFactory;
using Stats.SideStatsProvider;
using Stats.SideStatsProvider.HealthPoint;
using Stats.SideStatsProvider.ProtectionClass;

namespace Stats.Side
{
    public class ProtectionClass : IPhysicalResist
    {
        private float _value;

        private IPolicyThatStatsIsOver _policyThatStatsIsOver;
        private IPolicyThatStatsIsFilled _policyThatStatsIsFilled;
        private readonly IBasicStats _durability;
        private readonly IBasicStats _dexterity;

        private ISideStatProvider _sideStatProvider;
        
        public float Value => _value;

        public ProtectionClass(Durability durability, Dexterity dexterity, SideStatsValueFactory valueFactory, IPolicyThatStatsIsFilled policyThatStatsIsFilled, IPolicyThatStatsIsOver policyThatStatsIsOver)
        {
            _durability = durability;
            _dexterity = dexterity;
            _sideStatProvider = new ProtectionClassProvider(durability, dexterity, valueFactory);
            _policyThatStatsIsOver = policyThatStatsIsOver;
            _policyThatStatsIsFilled = policyThatStatsIsFilled;
            Calculate();
        }


        public void AddHandlers()
        {
            _durability.ValueChanged += Calculate;
            _dexterity.ValueChanged += Calculate;
        }

        public void RemoveHandlers()
        {
            _durability.ValueChanged -= Calculate;
            _dexterity.ValueChanged -= Calculate;
        }

        public void Calculate()
        {
            _value = _sideStatProvider.Calculate();
        }
    }
}