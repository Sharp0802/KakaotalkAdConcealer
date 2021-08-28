using System.Drawing;

using Windows.UI.ViewManagement;

namespace KakaotalkAdConcealer.Forms.Gui
{
    public static class ThemeDictionary
    {
        /// <summary>
        /// Windows ui settings
        /// </summary>
        private static UISettings Settings { get; } = new UISettings();

        /// <summary>
        /// Check theme user is using
        /// </summary>
        public static bool IsDarkTheme => Settings.GetColorValue(UIColorType.Background) is Windows.UI.Color { R: 0, G: 0, B: 0 };

        /// <summary>
        /// Windows background theme color
        /// </summary>
        public static Color ChromeMidium => IsDarkTheme ? Color.FromArgb(43, 43, 43) : Color.FromArgb(242, 242, 242);

        public static Color TextFillColorPrimary => IsDarkTheme ? Color.White : Color.FromArgb(23, 23, 23);

        public static Color ListLow => IsDarkTheme ? Color.FromArgb(65, 65, 65) : Color.FromArgb(217, 217, 217);

        public static Color ListMedium => IsDarkTheme ? Color.FromArgb(85, 85, 85) : Color.FromArgb(194, 194, 194);
    }
}
