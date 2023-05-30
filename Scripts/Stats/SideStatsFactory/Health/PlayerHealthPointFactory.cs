using Stats.Basic;
using Stats.Basic.Interface;
using Stats.SideStatsFactory;

namespace Stats.Side
{
    public class PlayerHealthPointFactory : SideStatsValueFactory
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
            return ((50 + basicStats.Value * 1.25f) + (level.Value + 1) / 2) * 0.2f;
        }

        public override float Create(params IBasicStats[] basicStats)
        {
            throw new System.NotImplementedException();
        }
    }
}