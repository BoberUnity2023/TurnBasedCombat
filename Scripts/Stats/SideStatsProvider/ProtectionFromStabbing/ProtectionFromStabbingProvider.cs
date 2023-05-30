﻿using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.ProtectionFromStabbing
{
    public class ProtectionFromStabbingProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;
        
        public ProtectionFromStabbingProvider(IBasicStats basicStats, Level level, SideStatsValueFactory sideStatsValueFactory)
        {
            _sideStatsValueFactory = sideStatsValueFactory;
            _basicStats = basicStats;
            _level = level;
        }
        
        public float Calculate()
        {
            return _sideStatsValueFactory.Create(_basicStats, _level);
        }
    }
}