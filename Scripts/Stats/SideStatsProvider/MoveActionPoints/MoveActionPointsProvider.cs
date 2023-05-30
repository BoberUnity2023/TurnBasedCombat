using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.MoveActionPoints
{
    public class MoveActionPointsProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        
        public MoveActionPointsProvider(SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
         
        }
        
        public  float Calculate()
        {
            return _sideStatsValueFactory.Create();
        }
    }
}