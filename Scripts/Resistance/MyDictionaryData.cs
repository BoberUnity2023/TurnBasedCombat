namespace Resistance
{
    public class MyDictionaryData<T , TK> where T : class
    {
        private readonly T _item1;
        private readonly TK _item2;

        public MyDictionaryData(T item1, TK item2)
        {
            _item1 = item1;
            _item2 = item2;
        }
            
        public T Item1 => _item1;

        public TK Item2 => _item2;
    }
}