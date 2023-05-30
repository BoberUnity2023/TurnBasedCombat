using Stats.Basic;
using Stats.Basic.Interface;

namespace Stats.SideStatsFactory
{
    public class EnemyMagicalResistanceDebuffsStatValueFactory : SideStatsValueFactory
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
            return (level.Value / 5f) + (basicStats.Value / 3f);
        }

        public override float Create(params IBasicStats[] basicStats)
        {
            throw new System.NotImplementedException();
        }
    }
    
    
}