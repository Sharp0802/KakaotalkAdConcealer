using System.Drawing;
using System.Windows.Forms;

using Windows.UI.ViewManagement;

namespace KakaotalkAdConcealer.Gui
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
		/// Windows background theme color
		/// </summary>
		public static Color Background
		{
			get
			{
				var color = Settings.GetColorValue(UIColorType.Background);
				return Color.FromArgb(color.A, color.R, color.G, color.B);
			}
		}

		/// <summary>
		/// Windows foreground theme color
		/// </summary>
		public static Color Foreground
		{
			get
			{
				var color = Settings.GetColorValue(UIColorType.Foreground);
				return Color.FromArgb(color.A, color.R, color.G, color.B);
			}
		}

		public override Color MenuItemBorder => Foreground;

		public override Color MenuItemSelected => Foreground;

		public override Color ToolStripDropDownBackground => Background;

		public override Color ImageMarginGradientBegin => Background;

		public override Color ImageMarginGradientMiddle => Background;

		public override Color ImageMarginGradientEnd => Background;
	}
}
