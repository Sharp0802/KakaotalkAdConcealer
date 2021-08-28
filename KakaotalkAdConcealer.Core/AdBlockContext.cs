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

        
        public void Dispose()
        {
            Source.Cancel();
            Source.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}