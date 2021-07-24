using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KakaotalkAdConcealer.Common;
using KakaotalkAdConcealer.Properties;

namespace KakaotalkAdConcealer.Concealer
{
    public class AdBlockContext : IDisposable
    {
        private CancellationTokenSource Source { get; } = new();
        private Task Blocker { get; set; }
        private ForceRef<bool> IsBlocking { get; } = new();

        private static void ShowInfo(string text, string caption)
        {
            MessageBox.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ToggleBlockState()
        {
            lock (IsBlocking)
            {
                var to = IsBlocking.Value = !IsBlocking.Value;
                SwitchBlockState(to);
                return to;
            }
        }
        
        public void SwitchBlockState(bool state)
        {
            if (state)
            {
                Blocker ??= ProcessAdBlocker.RemoveAllAds(Source.Token);
            }
            else
            {
                Source.Cancel();
                Blocker = null;
            }
        }
        
        public async Task BlockOnce(BlockType type)
        {
            switch (type)
            {
                case BlockType.Embedded:
                    await Task.Factory.StartNew(ProcessAdBlocker.RemoveAllEmbedAds)
                        .ContinueWith(_ => ShowInfo(Resources.CompletedTask, Resources.Completed));
                    break;
                case BlockType.Popup:
                    await Task.Factory.StartNew(ProcessAdBlocker.RemovePopupAd)
                        .ContinueWith(_ => ShowInfo(Resources.CompletedTask, Resources.Completed));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        
        public void Dispose()
        {
            Source.Cancel();
            Source.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}