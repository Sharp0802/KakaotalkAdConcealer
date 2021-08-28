using System;
using System.Linq;
using System.Reflection;

namespace KakaotalkAdConcealer.Forms.Common
{
    /// <summary>
    /// Helper for initializing outside of internal class
    /// </summary>
    /// <typeparam name="T">Type to initialize</typeparam>
    public class Initializable<T> where T : new()
    {
        /// <summary>
        /// Initialize properties in instance
        /// </summary>
        /// <param name="instance">Instance containing initializable properties</param>
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

        /// <summary>
        /// Current value of initializable
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Delegate that initialize value
        /// </summary>
        private Action<T> Initializer { get; }
    }
}