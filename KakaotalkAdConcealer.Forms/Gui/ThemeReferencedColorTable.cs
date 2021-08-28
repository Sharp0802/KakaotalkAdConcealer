using System.Drawing;
using System.Windows.Forms;
using Windows.UI.ViewManagement;

namespace KakaotalkAdConcealer.Forms.Gui
{
	/// <summary>
	/// Custom color table for making notifyicon context menu to use color by windows theme
	/// </summary>
	public class ThemeReferencedColorTable : ProfessionalColorTable
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
        public static Color Background => IsDarkTheme ? Color.FromArgb(43, 43, 43) : Color.FromArgb(242, 242, 242);

        /// <summary>
        /// Windows foreground theme color
        /// </summary>
        public static Color Foreground => IsDarkTheme ? Color.White : Color.Black;

        public override Color MenuItemBorder => Foreground;

		public override Color MenuItemSelected => Foreground;

		public override Color ToolStripDropDownBackground => Background;

		public override Color ImageMarginGradientBegin => Background;

		public override Color ImageMarginGradientMiddle => Background;

		public override Color ImageMarginGradientEnd => Background;
	}
}
