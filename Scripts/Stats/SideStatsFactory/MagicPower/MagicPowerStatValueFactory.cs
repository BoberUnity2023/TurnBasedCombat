using Stats.Basic;
using Stats.Basic.Interface;

namespace Stats.SideStatsFactory
{
    public class MagicPowerStatValueFactory : SideStatsValueFactory
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
            throw new System.NotImplementedException();
        }

        public override float Create(IBasicStats basicStats, Level level)
        {
            return (level.Value + 1) / 2 + (basicStats.Value / 10f);
        }

        public override float Create(params IBasicStats[] basicStats)
        {
            throw new System.NotImplementedException();
        }
    }
}