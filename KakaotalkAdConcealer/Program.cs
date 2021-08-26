using System.Threading;
using System.Windows.Forms;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer
{
    internal static class Program
    {
        // Keep it simple!
        private static void Main()
        {
            using var mutex = new Mutex(true, Resources.AppName, out var @new);
            if (@new)
            {
                using var _ = App.Create();
                Application.Run();
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(Resources.AlreadyActivated);
            }
        }
    }
}