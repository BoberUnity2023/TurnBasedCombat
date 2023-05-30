using System;

namespace Combat
{
    public class Round
    {
        private int _value;
        public int Value => _value;

        private Action _roundEnd;

        public void RoundEnd()
        {
            _value++;
            _roundEnd?.Invoke();
        }

        public void AddRoundEndHandlers(Action roundEnd)
        {
            _roundEnd += roundEnd;
        }

        public void RemoveRoundEndHandlers(Action roundEnd)
        {
            _roundEnd -= roundEnd;
        }

        public void BattleBegin()
        {
            _value = 1;
        }
        
    }
}