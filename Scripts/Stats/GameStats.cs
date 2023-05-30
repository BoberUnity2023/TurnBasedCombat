using System.Collections;
using System.Collections.Generic;
using Stats;
using Stats.Policy.NormalPolicy;
using UnityEngine;

public class GameStats
{
   private readonly BasicStats _basicStats;
   private readonly SideStats _sideStats;
   
   public BasicStats BasicStats => _basicStats;
   public SideStats SideStats => _sideStats;

   public GameStats()
   {
      _basicStats = new BasicStats(new PolicyThatStatsIsFilled(50));
      _sideStats = new SideStats(UnitType.Player, _basicStats);
   }
   
}
