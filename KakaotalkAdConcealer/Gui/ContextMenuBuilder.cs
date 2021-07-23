using System.Collections.Generic;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Gui
{
    public class ContextMenuBuilder
    {
        private List<ToolStripItem> Items { get; } = new();

        public ContextMenuBuilder Add(out ToolStripMenuItem item)
        {
            item = new ToolStripMenuItem();
            Items.Add(item);
            return this;
        }

        public ContextMenuBuilder AddSeparator()
        {
            var item = new ToolStripSeparator();
            Items.Add(item);
            return this;
        }

        public ContextMenuBuilder AddDropdown(out ToolStripDropDownItem item)
        {
            item = new ToolStripMenuItem();
            Items.Add(item);
            return this;
        }
        
        public ContextMenuStrip Build()
        {
            var menu = new ContextMenuStrip();
            menu.Items.AddRange(Items.ToArray());
            return menu;
        }
    }
}