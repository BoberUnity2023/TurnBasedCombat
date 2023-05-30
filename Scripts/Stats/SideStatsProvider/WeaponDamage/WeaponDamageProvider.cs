﻿using Stats.Basic;
using Stats.Basic.Interface;
using Stats.Effect;
using Stats.SideStatsFactory;

namespace Stats.SideStatsProvider.WeaponDamage
{
    public class WeaponDamageProvider : ISideStatProvider
    {
        private readonly SideStatsValueFactory _sideStatsValueFactory;
        private readonly IBasicStats _basicStats;
        private readonly Level _level;
        
        public WeaponDamageProvider(IBasicStats basicStats, Level level, SideStatsValueFactory sideStatsValueFactory)
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