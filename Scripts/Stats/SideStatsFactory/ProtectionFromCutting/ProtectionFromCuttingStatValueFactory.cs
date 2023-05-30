using Stats.Basic;
using Stats.Basic.Interface;

namespace Stats.SideStatsFactory
{
    public class ProtectionFromCuttingStatValueFactory : SideStatsValueFactory
    {
        public override float Create()
        {
            throw new System.NotImplementedException();
        }

        public override float Create(IBasicStats basicStats)
        {
            return basicStats.Value / 5f;
        }

        public override float Create(IBasicStats basicStats, IBasicStats basicStats2)
        {
            throw new System.NotImplementedException();
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