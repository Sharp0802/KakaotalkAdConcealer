using System.Windows.Forms;
using Microsoft.Win32;

namespace KakaotalkAdConcealer.Forms.Common
{
    /// <summary>
    /// Registry manager for auto startup
    /// </summary>
    public static class StartupRegister
    {
        /// <summary>
        /// Field for lazy initialize
        /// </summary>
        private static RegistryKey _registryKey;

        /// <summary>
        /// Resistry sub key for auto startup
        /// </summary>
        private static RegistryKey RegistryKey =>
            _registryKey ??= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        /// <summary>
        /// Force value registration with ingnoring current value
        /// </summary>
        /// <param name="name">Name of value</param>
        private static void ForceRegister(string name) => 
            RegistryKey.SetValue(name, Application.ExecutablePath);

        /// <summary>
        /// Force value unregistration with ingnoring current value
        /// </summary>
        /// <param name="name">Name of value</param>
        private static void ForceUnregister(string name) => 
            RegistryKey.DeleteValue(name, false);

        /// <summary>
        /// Get current state of value
        /// </summary>
        /// <param name="name">Name of value</param>
        public static bool IsAssigned(string name) => 
            RegistryKey.GetValue(name) is not null;

        /// <summary>
        /// Toggle value of registry
        /// </summary>
        /// <param name="name">Name of value</param>
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