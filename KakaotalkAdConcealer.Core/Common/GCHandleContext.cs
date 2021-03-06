using System;
using System.Runtime.InteropServices;

namespace KakaotalkAdConcealer.Core.Common
{
    /// <summary>
    /// GCHandle allocate/delete helper
    /// </summary>
    internal class GCHandleContext : IDisposable
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