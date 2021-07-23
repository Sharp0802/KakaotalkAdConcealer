using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using KakaotalkAdConcealer.Common;
using KakaotalkAdConcealer.Concealer;
using KakaotalkAdConcealer.Gui;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer
{
    public class App : IDisposable
    {
        private AdBlockContext Context { get; } = new();

        public NotifyIcon Startup()
        {
            var menu = new ContextMenuBuilder()
                .Add(out var title)
                .AddSeparator()
                .Add(out var startup)
                .AddSeparator()
                .Add(out var removeAll)
                .Add(out var removeEmbedded)
                .Add(out var removePopup)
                .AddSeparator()
                .Add(out var exit)
                .Build();

            title.Text = Resources.Title;
            title.ForeColor = Color.Gray;
            
            startup.Text = Resources.ActivateAtStartup;
            startup.Checked = StartupRegister.IsAssigned(Resources.AppName);
            startup.Click += (_, _) => startup.Checked = StartupRegister.Toggle(Resources.AppName);

            removeAll.Text = Resources.RemoveAllAds;
            removeAll.Click += (_, _) => removeAll.Checked = Context.ToggleBlockState();
            
            removeEmbedded.Text = Resources.RemoveEmbeddedAdsOnce;
            removeEmbedded.Click += (_, _) => _ = Context.BlockOnce(BlockType.Embedded);
            
            removePopup.Text = Resources.RemovePopupAdsOnce;
            removePopup.Click += (_, _) => _ = Context.BlockOnce(BlockType.Popup);

            exit.Text = Resources.Quit;
            exit.Click += (_, _) =>
            {
                Application.Exit();
                Environment.Exit(0);
            };

            return new NotifyIcon
            {
                Visible = true,
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                ContextMenuStrip = menu
            };
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
