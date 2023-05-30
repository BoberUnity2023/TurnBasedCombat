using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.ProtectionClass
{
    public class ProtectionClassProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly IBasicStats _durability;
        private readonly IBasicStats _dexterity;
        
        public ProtectionClassProvider(Durability durability, Dexterity dexterity, SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
            _durability = durability;
            _dexterity = dexterity;
        }
        
        public  float Calculate()
        {
            return _sideStatsValueFactory.Create(_durability, _dexterity);
        }
    }
}