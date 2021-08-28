using System.Drawing;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Forms.Gui
{
    using static ThemeDictionary;

	/// <summary>
	/// Custom color table for making notifyicon context menu to use color by windows theme
	/// </summary>
	public class ThemeReferencedColorTable : ProfessionalColorTable
	{
        public override Color ButtonCheckedGradientBegin => ListLow;

        public override Color ButtonCheckedGradientEnd => ListLow;

        public override Color ButtonCheckedGradientMiddle => ListLow;

        public override Color ButtonCheckedHighlight => ListLow;

        public override Color ButtonCheckedHighlightBorder => ListLow;

        public override Color ButtonPressedBorder => ListMedium;

        public override Color ButtonPressedGradientBegin => ListMedium;

        public override Color ButtonPressedGradientEnd => ListMedium;

        public override Color ButtonPressedGradientMiddle => ListMedium;

        public override Color ButtonPressedHighlight => ListMedium;

        public override Color ButtonPressedHighlightBorder => ListMedium;

        public override Color ButtonSelectedBorder => ListLow;

        public override Color ButtonSelectedGradientBegin => ListLow;

        public override Color ButtonSelectedGradientEnd => ListLow;

        public override Color ButtonSelectedGradientMiddle => ListLow;

        public override Color ButtonSelectedHighlight => ListLow;

        public override Color ButtonSelectedHighlightBorder => ListLow;

        public override Color CheckBackground => ListLow;

        public override Color CheckPressedBackground => ListMedium;

        public override Color CheckSelectedBackground => ListLow;

        public override Color MenuBorder => ListLow;

        public override Color MenuItemBorder => ListLow;

        public override Color MenuItemPressedGradientBegin => ListMedium;

		public override Color MenuItemPressedGradientEnd => ListMedium;

        public override Color MenuItemPressedGradientMiddle => ListMedium;

        public override Color MenuItemSelected => ListLow;

        public override Color MenuItemSelectedGradientBegin => ListLow;

        public override Color MenuItemSelectedGradientEnd => ListLow;

        public override Color MenuStripGradientBegin => ChromeMidium;

        public override Color MenuStripGradientEnd => ChromeMidium;

        public override Color ToolStripDropDownBackground => ChromeMidium;

		public override Color ImageMarginGradientBegin => ChromeMidium;

		public override Color ImageMarginGradientEnd => ChromeMidium;

		public override Color ImageMarginGradientMiddle => ChromeMidium;

        public override Color ImageMarginRevealedGradientBegin => ListLow;

        public override Color ImageMarginRevealedGradientEnd => ListLow;

        public override Color ImageMarginRevealedGradientMiddle => ListLow;

        public override Color OverflowButtonGradientBegin => ListLow;

        public override Color OverflowButtonGradientEnd => ListLow;

        public override Color OverflowButtonGradientMiddle => ListLow;

        public override Color RaftingContainerGradientBegin => ChromeMidium;

        public override Color RaftingContainerGradientEnd => ChromeMidium;

        public override Color SeparatorDark => ListMedium;

        public override Color SeparatorLight => TextFillColorPrimary;

        public override Color StatusStripGradientBegin => ChromeMidium;

        public override Color StatusStripGradientEnd => ChromeMidium;

        public override Color ToolStripBorder => ChromeMidium;

        public override Color ToolStripContentPanelGradientBegin => ChromeMidium;

        public override Color ToolStripContentPanelGradientEnd => ChromeMidium;

        public override Color ToolStripGradientBegin => ChromeMidium;

        public override Color ToolStripGradientEnd => ChromeMidium;

        public override Color ToolStripGradientMiddle => ChromeMidium;

        public override Color ToolStripPanelGradientBegin => ChromeMidium;

        public override Color ToolStripPanelGradientEnd => ChromeMidium;
    }
}
