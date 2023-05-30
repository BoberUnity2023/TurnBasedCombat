﻿using Stats.Basic;
using Stats.Basic.Interface;

namespace Stats.SideStatsFactory
{
    public class MoveActionPointsStatValueFactory : SideStatsValueFactory
    {
        public override float Create()
        {
            return 9;
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
            throw new System.NotImplementedException();
        }

        public override float Create(params IBasicStats[] basicStats)
        {
            throw new System.NotImplementedException();
        }
    }
}