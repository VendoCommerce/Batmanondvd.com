using System;

namespace CSCore
{
    [Serializable]
    public class Pair<T, U> : IEquatable<Pair<T, U>>
    {
        private T _Item1;
        private U _Item2;

       
        public Pair()
        {
        }

        public Pair(T item1, U item2)
        {
            this._Item1 = item1;
            this._Item2 = item2;
        }

        public T Item1
        {
            get { return _Item1; }
            set { _Item1 = value; }
        }

        public U Item2
        {
            get { return _Item2; }
            set { _Item2 = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj is Pair<T, U>)
            {
                return Equals((Pair<T, U>)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _Item1.GetHashCode() ^ _Item2.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Pair<{0}, {1}>", Item1, Item2);
        }

        #region IEquatable<Pair<T,U>> Members

        public bool Equals(Pair<T, U> other)
        {
            return this._Item1.Equals(other._Item1) && this._Item2.Equals(other._Item2);
        }

        #endregion
    }
}
