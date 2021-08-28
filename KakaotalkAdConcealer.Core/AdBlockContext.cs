using KakaotalkAdConcealer.Common;

namespace KakaotalkAdConcealer.Concealer
{
    /// <summary>
    /// Helper for blocking ads
    /// </summary>
    public class AdBlockContext : IDisposable
    {
        /// <summary>
        /// CancellationTokenSource for long executing methods
        /// </summary>
        private CancellationTokenSource Source { get; } = new();
        
        /// <summary>
        /// Property containing blocking state
        /// </summary>
        private ForceRef<bool> IsBlocking { get; } = new();

        /// <summary>
        /// Helper for showing messagebox with text and caption
        /// </summary>
        /// <param name="text">Text of messagebox</param>
        /// <param name="caption">Caption of messagebox</param>
        private static void ShowInfo(string text, string caption)
        {
            MessageBox.Show(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Toggle blocking state
        /// </summary>
        /// <returns>Value of blocking state</returns>
        public bool ToggleBlockState()
        {
            lock (IsBlocking)
            {
                var to = IsBlocking.Value = !IsBlocking.Value;
                SwitchBlockState(to);
                return to;
            }
        }
        
        /// <summary>
        /// Set blocking state
        /// </summary>
        /// <param name="state">Value to set</param>
        public void SwitchBlockState(bool state)
        {
            if (state)
            {
                _ = ProcessAdBlocker.RemoveAllAds(Source.Token);
            }
            else
            {
                Source.Cancel();
            }
        }
        
        /// <summary>
        /// Block ads like param once
        /// </summary>
        /// <param name="type">Value of blocking type</param>
        public async Task BlockOnce(BlockType type)
        {
            switch (type)
            {
                case BlockType.Embedded:
                    await Task.Factory.StartNew(() => ProcessAdBlocker.RemoveAllEmbedAds(CancellationToken.None))
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