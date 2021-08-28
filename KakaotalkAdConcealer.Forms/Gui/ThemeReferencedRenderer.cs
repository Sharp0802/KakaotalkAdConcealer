using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Forms.Gui
{
	/// <summary>
	/// Custom renderer for making notifyicon context menu to use color by windows theme
	/// </summary>
	public class ThemeReferencedRenderer : ToolStripProfessionalRenderer
	{
		public int VerticalPadding { get; init; }

		public ThemeReferencedRenderer() : base(new ThemeReferencedColorTable()) { }

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs args)
		{
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			var r = new Rectangle(args.ArrowRectangle.Location, args.ArrowRectangle.Size);
			r.Inflate(-2, -4);
			using var pen = new Pen(ThemeDictionary.TextFillColorPrimary);
			args.Graphics.DrawLines(pen, new Point[] {
				new Point(r.Left, r.Top),
				new Point(r.Right, r.Top + r.Height / 2),
				new Point(r.Left, r.Top+ r.Height)
			});
		}

		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs args)
		{
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			var r = new Rectangle(args.ImageRectangle.Location, args.ImageRectangle.Size);
			r.Inflate(-2, -4);
			using var pen = new Pen(ThemeDictionary.TextFillColorPrimary);
			args.Graphics.DrawLines(pen, new Point[] {
				new Point(r.Left, r.Bottom - r.Height / 2),
				new Point((int)(r.Left + r.Width / 2.5f),  r.Bottom),
				new Point(r.Right, r.Top)
			});
		}

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs args)
        {
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			var r = args.Item.ContentRectangle;
			using var pen = new Pen(ThemeDictionary.BaseMedium);
			args.Graphics.DrawLines(pen, new Point[]
			{
				new Point(r.Left + 5, (r.Bottom + r.Top) / 2),
				new Point(r.Right - 5, (r.Bottom + r.Top) / 2)
			});
		}

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs args)
		{
			args.TextFormat &= ~TextFormatFlags.HidePrefix;
			args.TextFormat |= TextFormatFlags.VerticalCenter;
			var rect = args.TextRectangle;
			rect.Offset(0, VerticalPadding);
			args.TextRectangle = rect;
			base.OnRenderItemText(args);
		}

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs args)
		{
			if (args.Item.Selected)
            {
				args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				using var brush = new SolidBrush(ThemeDictionary.ListMedium);
				args.Graphics.FillRectangle(brush, new Rectangle(2, 0, args.Item.Width - 4, args.Item.Height - 1));
            }
		}
    }
}
