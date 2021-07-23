using System.Windows.Forms;
using Microsoft.Win32;

namespace KakaotalkAdConcealer.Common
{
    public static class StartupRegister
    {
        private static RegistryKey _registryKey;
        private static RegistryKey RegistryKey =>
            _registryKey ??= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        private static void ForceRegister(string name) => 
            RegistryKey.SetValue(name, Application.ExecutablePath);

        private static void ForceUnregister(string name) => 
            RegistryKey.DeleteValue(name, false);

        public static bool IsAssigned(string name) => 
            RegistryKey.GetValue(name) is not null;

        public static bool Toggle(string name)
        {
            var assigned = IsAssigned(name);
            if (assigned)
                ForceUnregister(name);
            else
                ForceRegister(name);
            return !assigned;
        }
    }
}