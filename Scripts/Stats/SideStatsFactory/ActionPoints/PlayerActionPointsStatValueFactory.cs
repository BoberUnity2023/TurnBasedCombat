using Stats.Basic;
using Stats.Basic.Interface;

namespace Stats.SideStatsFactory
{
    public class PlayerActionPointsStatValueFactory : SideStatsValueFactory
    {
        public override float Create()
        {
            throw new System.NotImplementedException();
        }

        public override float Create(IBasicStats basicStats)
        {
            throw new System.NotImplementedException();
        }

        public override float Create(IBasicStats basicStats, IBasicStats basicStats2)
        {
            return 2 + (2 * basicStats.Value) + (basicStats2.Value / 25);
        }

        public override float Create(IBasicStats basicStats, Level level)
        {
            throw new System.NotImplementedException();
        }

        public override float Create(params IBasicStats[] basicStats)
        {
            throw new System.NotImplementedException();
        }
    }
}