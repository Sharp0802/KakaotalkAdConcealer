using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace KakaotalkAdConcealer.Common
{
    [type: SuppressMessage("ReSharper", "InconsistentNaming")]
    public class GCHandleContext : IDisposable
    {
        public GCHandle Handle { get; }

        public IntPtr Pointer => GCHandle.ToIntPtr(Handle);

        public GCHandleContext(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
            Handle = GCHandle.Alloc(obj);
        }

        public void Dispose()
        {
            if (Handle.IsAllocated)
                Handle.Free();
            GC.SuppressFinalize(this);
        }
    }
}