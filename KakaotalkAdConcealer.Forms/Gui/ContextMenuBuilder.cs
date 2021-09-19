using System.Collections.Generic;
using Microsoft.Win32;

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
            Items.Add(new ToolStripSeparator { Margin = new Padding(0, 2, 0, 2) });
            return this;
        }
        
        /// <summary>
        /// Build targeted menu items
        /// </summary>
        /// <returns>Builded menu strip</returns>
        public ContextMenuStrip Build()
        {
            const int padding = 5;

            var menu = new ContextMenuStrip
            {
                Renderer = new ThemeReferencedRenderer { VerticalPadding = padding },
                ForeColor = ThemeDictionary.TextFillColorPrimary
            };
            SystemEvents.UserPreferenceChanged += (sender, args) => menu.ForeColor = ThemeDictionary.TextFillColorPrimary;
            var array = Items.ToArray();
            for (int i = 0; i < array.Length; ++i)
            {
                array[i].Padding = new Padding(0, padding, 0, padding);
                array[i].Margin += new Padding(0, i == 0 ? 5 : 0, 0, i == array.Length - 1 ? 5 : 0);
            }
            menu.Items.AddRange(array);
            return menu;
        }
    }
}