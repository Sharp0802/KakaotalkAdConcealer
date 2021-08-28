using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Gui
{
	/// <summary>
	/// Custom renderer for making notifyicon context menu to use color by windows theme
	/// </summary>
	public class ThemeReferencedRenderer : ToolStripProfessionalRenderer
	{
		/// <summary>
		/// Color table using color by windows theme
		/// </summary>
		private static ThemeReferencedColorTable ThemeColorTable { get; } = new ThemeReferencedColorTable();

		public ThemeReferencedRenderer() : base(ThemeColorTable) { }

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs args)
		{
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			var r = new Rectangle(args.ArrowRectangle.Location, args.ArrowRectangle.Size);
			r.Inflate(-2, -6);
			using var pen = new Pen(ThemeReferencedColorTable.Foreground);
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
			r.Inflate(-2, -3);
			using var pen = new Pen(ThemeReferencedColorTable.Foreground);
			args.Graphics.DrawLines(pen, new Point[] {
				new Point(r.Left, r.Bottom - r.Height / 2),
				new Point((int)(r.Left + r.Width / 2.5f),  r.Bottom),
				new Point(r.Right, r.Top)
			});
		}
	}
}
