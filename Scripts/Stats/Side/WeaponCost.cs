namespace Stats.Side
{
    public class WeaponCost
    {
        private  int _value;
        
        public WeaponCost(int value)
        {
            _value = value;
        }
        
        public void SetCost(int value)
        {
            _value = value;
        }
    }
}