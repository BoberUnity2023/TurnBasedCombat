using Stats.Basic;
using Stats.Basic.Interface;
using UnityEngine;

namespace Stats.SideStatsFactory
{
    public abstract class SideStatsValueFactory
    {
        public abstract float Create();
        public abstract float Create(IBasicStats basicStats);
        public abstract float Create(IBasicStats basicStats , IBasicStats basicStats2);
        public abstract float Create(IBasicStats basicStats , Level level);
        public abstract float Create(params IBasicStats[] basicStats);
    }
}
