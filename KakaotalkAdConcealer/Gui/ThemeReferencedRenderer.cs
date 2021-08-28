using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Gui
{
	public class ThemeReferencedRenderer : ToolStripProfessionalRenderer
	{
		private static ThemeReferencedColorTable ThemeColorTable { get; } = new ThemeReferencedColorTable();

		public ThemeReferencedRenderer() : base(ThemeColorTable) { }

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs args)
		{
			args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			var r = new Rectangle(args.ArrowRectangle.Location, args.ArrowRectangle.Size);
			r.Inflate(-2, -6);
			using var pen = new Pen(ThemeReferencedColorTable.Smoke);
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
			r.Inflate(-4, -6);
			using var pen = new Pen(ThemeReferencedColorTable.Smoke);
			args.Graphics.DrawLines(pen, new Point[] {
				new Point(r.Left, r.Bottom - r.Height / 2),
				new Point(r.Left + r.Width / 3,  r.Bottom),
				new Point(r.Right, r.Top)
			});
		}
	}
}
