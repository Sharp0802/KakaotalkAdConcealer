using System.Drawing;
using System.Windows.Forms;

using Windows.UI.ViewManagement;

namespace KakaotalkAdConcealer.Gui
{
	public class ThemeReferencedColorTable : ProfessionalColorTable
	{
		private static UISettings Settings { get; } = new UISettings();

		public static Color Background
		{
			get
			{
				var color = Settings.GetColorValue(UIColorType.Background);
				return Color.FromArgb(color.A, color.R, color.G, color.B);
			}
		}

		public static Color Smoke
		{
			get
			{
				var color = Settings.GetColorValue(UIColorType.Foreground);
				return Color.FromArgb(color.A, color.R, color.G, color.B);
			}
		}

		public override Color MenuItemBorder => Smoke;

		public override Color MenuItemSelected => Smoke;

		public override Color ToolStripDropDownBackground => Background;

		public override Color ImageMarginGradientBegin => Background;

		public override Color ImageMarginGradientMiddle => Background;

		public override Color ImageMarginGradientEnd => Background;
	}
}
