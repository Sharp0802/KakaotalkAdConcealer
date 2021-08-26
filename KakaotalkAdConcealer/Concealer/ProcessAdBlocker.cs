using System.Diagnostics;
using System.Runtime.InteropServices;
using KakaotalkAdConcealer.Common;
using KakaotalkAdConcealer.Native;

namespace KakaotalkAdConcealer.Concealer
{
    public static class ProcessAdBlocker
    {
        private const int UpdateRate = 100;

        private const uint WmClose = 0x10;

        private const int ShadowPadding = 2;

        private const int MainViewPadding = 31;


        public static Task RemoveAllAds(CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    RemoveAllEmbedAds(token);
                    RemovePopupAd();
                    Task.Delay(UpdateRate, token).Wait(token);
                }
            }, token);
        }

        public static void RemovePopupAd()
        {
            var popUp = Win32.FindWindow(IntPtr.Zero, IntPtr.Zero, null, "");
            if (Win32.GetParent(popUp) != IntPtr.Zero ||
                !Win32.GetClassName(popUp).Contains("RichPopWnd"))
                return;

            var rect = Win32.GetWindowRect(popUp);
            if (rect.Right - rect.Left is 300 && rect.Bottom - rect.Top is 150)
                Win32.SendMessage(popUp, WmClose);
        }

        public static void RemoveAllEmbedAds(CancellationToken token)
        {
            foreach (var kakaotalk in Process.GetProcessesByName("kakaotalk"))
            {
                if (token.IsCancellationRequested)
                    break;
                RemoveEmbedAds(kakaotalk, token);
            }
        }

        public static void RemoveEmbedAds(Process process, CancellationToken token)
        {
            var kakaotalk = process.MainWindowHandle;
            if (kakaotalk == IntPtr.Zero)
                return;

            var children = new List<IntPtr>();
            using (var context = new GCHandleContext(children))
            {
                static bool EnumChildWindows(IntPtr handle, IntPtr param)
                {
                    if (GCHandle.FromIntPtr(param).Target is not List<IntPtr> list)
                        return false;
                    list.Add(handle);
                    return true;
                }

                Win32.EnumChildWindows(kakaotalk, EnumChildWindows, context.Pointer);
            }

            var rect = Win32.GetWindowRect(kakaotalk);
            foreach (var child in children)
            {
                if (token.IsCancellationRequested)
                    break;
                if (Win32.GetParent(child) != kakaotalk)
                    continue;

                var @class = Win32.GetClassName(child);
                var caption = Win32.GetWindowText(child);

                Win32.UpdateWindow(kakaotalk);

                HideMainViewAd(@class, kakaotalk, child);
                HideMainViewAdArea(caption, rect, child);
                HideLockViewAdArea(caption, rect, child);
            }
        }

        private static void HideMainViewAd(string @class, IntPtr kakaotalk, IntPtr child)
        {
            if (@class is not "BannerAdWnd" and not "EVA_Window" ||
                !Win32.GetWindowText(kakaotalk).StartsWith("LockModeView"))
                return;
            Win32.ShowWindow(child, ShowingFlag.Hide);
            Win32.SetWindowPos(child, IntPtr.Zero, new Vector(0, 0), new Vector(0, 0), SizingFlag.NoMove);
        }

        private static void HideMainViewAdArea(string caption, Rect rect, IntPtr child)
        {
            if (!caption.StartsWith("OnlineMainView"))
                return;
            var size = new Vector(rect.Right - rect.Left - ShadowPadding, rect.Bottom - rect.Top - MainViewPadding);
            Win32.SetWindowPos(child, IntPtr.Zero, new Vector(0, 0), size, SizingFlag.NoMove);
        }

        private static void HideLockViewAdArea(string caption, Rect rect, IntPtr child)
        {
            if (!caption.StartsWith("LockModeView"))
                return;
            var size = new Vector(rect.Right - rect.Left - ShadowPadding, rect.Bottom - rect.Top);
            Win32.SetWindowPos(child, IntPtr.Zero, new Vector(0, 0), size, SizingFlag.NoMove);
        }
    }
}