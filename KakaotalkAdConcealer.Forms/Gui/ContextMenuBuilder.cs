using System.Windows.Forms;

namespace KakaotalkAdConcealer.Forms.Gui
{
    /// <summary>
    /// Helper for making menu with method chaining
    /// </summary>
    public class ContextMenuBuilder
    {
        /// <summary>
        /// Added items
        /// </summary>
        private List<ToolStripItem> Items { get; } = new();

        /// <summary>
        /// Add menu item to build target
        /// </summary>
        /// <param name="item">Menu item</param>
        public ContextMenuBuilder Add(ToolStripMenuItem item)
        {
            Items.Add(item);
            return this;
        }

        /// <summary>
        /// Add menu seperator to build target
        /// </summary>
        public ContextMenuBuilder AddSeparator()
        {
            var item = new ToolStripSeparator();
            Items.Add(item);
            return this;
        }
        
        /// <summary>
        /// Build targeted menu items
        /// </summary>
        /// <returns>Builded menu strip</returns>
        public ContextMenuStrip Build()
        {
            var menu = new ContextMenuStrip 
            { 
                Renderer = new ThemeReferencedRenderer(),
                BackColor = ThemeReferencedColorTable.Background,
                ForeColor = ThemeReferencedColorTable.Foreground
            };
            menu.Items.AddRange(Items.ToArray());
            return menu;
        }
    }
}