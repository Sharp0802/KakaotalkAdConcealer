using System.Collections.Generic;
using System.Windows.Forms;

namespace KakaotalkAdConcealer.Gui
{
    public class ContextMenuBuilder
    {
        private List<ToolStripItem> Items { get; } = new();

        public ContextMenuBuilder Add(ToolStripMenuItem item)
        {
            Items.Add(item);
            return this;
        }

        public ContextMenuBuilder AddSeparator()
        {
            var item = new ToolStripSeparator();
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