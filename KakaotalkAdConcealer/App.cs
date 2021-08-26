using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using KakaotalkAdConcealer.Common;
using KakaotalkAdConcealer.Concealer;
using KakaotalkAdConcealer.Gui;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer
{
    /// <summary>
    /// Application instance
    /// </summary>
    public class App : IDisposable
    {
        /// <summary>
        /// Instance initialization locking handle
        /// </summary>
        private static object LockHandle { get; } = new();

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static App Instance { get; set; }

        /// <summary>
        /// Ads blocking context
        /// </summary>
        private AdBlockContext Context { get; } = new();

        /// <summary>
        /// Event for localization
        /// </summary>
        private event Action<CultureInfo> CultureUpdated;

        /// <summary>
        /// Activate application
        /// </summary>
        /// <returns>Application instance</returns>
        public static App Create()
        {
            lock (LockHandle)
            {
                Instance = Instance is null
                    ? new App()
                    : throw new InvalidProgramException();
                
                Initializable<ToolStripMenuItem>.Initialize(Instance);
                Instance.CultureUpdated?.Invoke(CultureInfo.InvariantCulture);
                _ = new NotifyIcon
                {
                    Visible = true,
                    Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                    ContextMenuStrip = new ContextMenuBuilder()
                        .Add(Instance.Title.Value)
                        .AddSeparator()
                        .Add(Instance.Startup.Value)
                        .Add(Instance.Language.Value)
                        .AddSeparator()
                        .Add(Instance.RemoveAll.Value)
                        .Add(Instance.RemoveEmbedded.Value)
                        .Add(Instance.RemovePopup.Value)
                        .AddSeparator()
                        .Add(Instance.Quit.Value)
                        .Build()
                };

                InfoNotifier.ShowRunningNotification();

                return Instance;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Menu item for showing title
        /// </summary>
        private Initializable<ToolStripMenuItem> Title { get; } = new(title =>
        {
            Instance.CultureUpdated += _ => title.Text = Resources.Title;
            title.ForeColor = Color.Gray;
        });

        /// <summary>
        /// Menu item for showing/switching auto startup state
        /// </summary>
        private Initializable<ToolStripMenuItem> Startup { get; } = new(startup =>
        {
            Instance.CultureUpdated += _ => startup.Text = Resources.ActivateAtStartup;
            startup.Checked = StartupRegister.IsAssigned(Resources.AppName);
            startup.Click += (_, _) => startup.Checked = StartupRegister.Toggle(Resources.AppName);
        });

        /// <summary>
        /// Menu item for showing/selecting language in language list
        /// </summary>
        private Initializable<ToolStripMenuItem> Language { get; } = new(language =>
        {
            Instance.CultureUpdated += _ =>
                language.Text = (Resources.Culture ?? CultureInfo.InvariantCulture).ToISOCode();
            var cultures = LanguageExtension.GetAvailableCultures();
            foreach (var culture in cultures)
            {
                var item = new ToolStripMenuItem(culture.ToISOCode());
                item.Click += (_, _) =>
                {
                    Resources.Culture = culture;
                    Instance.CultureUpdated?.Invoke(culture);
                };
                language.DropDownItems.Add(item);
            }
        });

        /// <summary>
        /// Menu item for showing/switching ads blocking state
        /// </summary>
        private Initializable<ToolStripMenuItem> RemoveAll { get; } = new(removeAll =>
        {
            Instance.CultureUpdated += _ => removeAll.Text = Resources.RemoveAllAds;
            removeAll.Click += (_, _) => removeAll.Checked = Instance.Context.ToggleBlockState();
        });

        /// <summary>
        /// Menu item for removing embedded ads manually
        /// </summary>
        private Initializable<ToolStripMenuItem> RemoveEmbedded { get; } = new(removeEmbedded =>
        {
            Instance.CultureUpdated += _ => removeEmbedded.Text = Resources.RemoveEmbeddedAdsOnce;
            removeEmbedded.Click += (_, _) => _ = Instance.Context.BlockOnce(BlockType.Embedded);
        });

        /// <summary>
        /// Menu item for removing popup ads manually
        /// </summary>
        private Initializable<ToolStripMenuItem> RemovePopup { get; } = new(removePopup =>
        {
            Instance.CultureUpdated += _ => removePopup.Text = Resources.RemovePopupAdsOnce;
            removePopup.Click += (_, _) => _ = Instance.Context.BlockOnce(BlockType.Popup);
        });

        /// <summary>
        /// Menu item for exit this application
        /// </summary>
        private Initializable<ToolStripMenuItem> Quit { get; } = new(quit =>
        {
            Instance.CultureUpdated += _ => quit.Text = Resources.Quit;
            quit.Click += (_, _) =>
            {
                Application.Exit();
                Environment.Exit(0);
            };
        });
    }
}