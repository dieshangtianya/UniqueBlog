using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Infrastructure
{
    public class EntityEqualityCompare<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _equalCompareFunct;

        public EntityEqualityCompare(Func<T,T,bool> equalCompareFunc)
        {
            _equalCompareFunct = equalCompareFunc;
        }

        public bool Equals(T x, T y)
        {
            return _equalCompareFunct(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
