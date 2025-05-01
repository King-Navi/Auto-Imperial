using System.Reflection;

namespace WpfClient.Utilities
{
    public class GenericComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            if (x == null || y == null)
            {
                Console.WriteLine($"Propiedad diferente:  - value1: {x} | value2: {y}");
                return false;

            }

            foreach (PropertyInfo prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                //Ignore collections?????
                //if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                //    continue;

                var value1 = prop.GetValue(x);
                var value2 = prop.GetValue(y);

                if (value1 == null && value2 == null)
                    continue;

                // Comparar listas por contenido si es una colección
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    var list1 = value1 as System.Collections.IEnumerable;
                    var list2 = value2 as System.Collections.IEnumerable;

                    if (list1 == null || list2 == null)
                        return false;

                    var arr1 = list1.Cast<object>().ToArray();
                    var arr2 = list2.Cast<object>().ToArray();

                    if (!arr1.SequenceEqual(arr2))
                    {
                        Console.WriteLine($"Lista diferente en {prop.Name}");
                        return false;
                    }

                    continue;
                }

                if (value1 == null || value2 == null || !value1.Equals(value2))
                {
                    Console.WriteLine($"Propiedad diferente: {prop.Name} - value1: {value1} | value2: {value2}");
                    return false;
                }
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
}