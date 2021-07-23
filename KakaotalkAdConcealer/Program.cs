using System.Threading;
using System.Windows.Forms;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer
{
    internal static class Program
    {
        private static void Main()
        {
            using var mutex = new Mutex(true, Resources.AppName, out var @new);
            if (@new)
            {
                new App().Startup();
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