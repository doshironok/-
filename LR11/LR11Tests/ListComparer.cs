using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibraryTests
{
    public class ListComparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T> x, List<T> y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<T> obj)
        {
            return obj?.Aggregate(0, (hash, item) => hash ^ (item?.GetHashCode() ?? 0)) ?? 0;
        }
    }
}
