using System;
using System.Threading;
using System.Windows.Forms;
using KakaotalkAdConcealer.Forms.Properties;

namespace KakaotalkAdConcealer.Forms
{
    internal static class Program
    {
        // Keep it simple!
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            using var mutex = new Mutex(true, App.AppName, out var @new);
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

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // ReSharper disable once LocalizableElement
            MessageBox.Show(null, args.ExceptionObject.ToString(), "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}