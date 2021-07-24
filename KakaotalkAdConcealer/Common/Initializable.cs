using System;
using System.Linq;
using System.Reflection;

namespace KakaotalkAdConcealer.Common
{
    public class Initializable<T> where T : new()
    {
        public static void Initialize(object instance)
        {
            foreach (var initializer in instance
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(property => property.PropertyType == typeof(Initializable<T>))
                .Select(property => property.GetMethod?.CreateDelegate<Func<Initializable<T>>>(instance)))
            {
                var initializable = initializer?.Invoke() 
                                    ?? throw new NullReferenceException("Initializer is null");
                initializable.Initializer.Invoke(initializable.Value);
            }
        }
        
        public Initializable(Action<T> initializer)
        {
            Value = new T();
            Initializer = initializer;
        }

        public T Value { get; }
        private Action<T> Initializer { get; }
    }
}