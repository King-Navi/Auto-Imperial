using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Utilities
{
    public class GenericComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            if (x == null || y == null)
                return false;

            foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value1 = prop.GetValue(x);
                var value2 = prop.GetValue(y);

                if (value1 == null && value2 == null)
                    continue;

                if (value1 == null || value2 == null || !value1.Equals(value2))
                    return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            if (obj == null)
                return 0;

            int hash = 17;
            foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(obj);
                hash = hash * 23 + (value?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
