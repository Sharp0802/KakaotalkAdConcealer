using System.Runtime.InteropServices;

namespace KakaotalkAdConcealer.Common
{
    /// <summary>
    /// GCHandle allocate/delete helper
    /// </summary>
    public class GCHandleContext : IDisposable
    {
        /// <summary>
        /// Allocated handle for object
        /// </summary>
        public GCHandle Handle { get; }

        /// <summary>
        /// Pointer of handle
        /// </summary>
        public IntPtr Pointer => GCHandle.ToIntPtr(Handle);

        public GCHandleContext(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
            Handle = GCHandle.Alloc(obj);
        }

        ~GCHandleContext() => Dispose();

        public void Dispose()
        {
            if (Handle.IsAllocated)
                Handle.Free();
            GC.SuppressFinalize(this);
        }
    }
}