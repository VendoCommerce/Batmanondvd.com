using System;

namespace CSCore
{
    [Serializable]
    public class Triplet<T, U, V>
    {
        private T _Item1;
        private U _Item2;
        private V _Item3;

        public Triplet(T item1, U item2, V item3)
        {
            _Item1 = item1;
            _Item2 = item2;
            _Item3 = item3;
        }


        public T Item1 { get { return _Item1; } set { _Item1 = value; } }
        public U Item2 { get { return _Item2; } set { _Item2 = value; } }
        public V Item3 { get { return _Item3; } set { _Item3 = value; } }

    }
}
