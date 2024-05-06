using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace MdiViewHelper
{
    public static class UIMenuUtility
    {
        // Modified to return a ToolStripMenuItem
        public static ToolStripMenuItem CreateMenu(MenuModel menu, Func<string, EventHandler> createClickHandler)
        {
            var menuItem = CreateMenuItem(menu, createClickHandler(menu.MenuURL));
            AddChildMenuItems(menuItem, menu.Children, createClickHandler);
            return menuItem;
        }

        private static ToolStripMenuItem CreateMenuItem(MenuModel menu, EventHandler clickHandler)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = menu.MenuName,
                Tag = menu.MenuURL
            };

            if (menu.MenuURL != null)
            {
                menuItem.Click += clickHandler;
            }
            return menuItem;
        }

        private static void AddChildMenuItems(ToolStripMenuItem parentMenuItem, List<MenuModel> children, Func<string, EventHandler> createClickHandler)
        {
            if (children == null) return;

            foreach (var child in children)
            {
                var childMenuItem = CreateMenuItem(child, createClickHandler(child.MenuURL));
                parentMenuItem.DropDownItems.Add(childMenuItem);
                AddChildMenuItems(childMenuItem, child.Children, createClickHandler);
            }
        }
    }
    public class MenuModel
    {
        [Category("Menu Settings"), DisplayName("Menu Name"), Description("The display name of the menu item.")]
        public required string MenuName { get; set; }

        [Category("Menu Settings"), DisplayName("Menu URL"), Description("URL of the menu item. Can be null if the menu item has children.")]
        public string? MenuURL { get; set; }

        [Category("Menu Settings"), DisplayName("Sub Menus"), Description("Child menu items of this menu.")]
        [Editor(typeof(CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<MenuModel> Children { get; set; } = new List<MenuModel>();

        public override string ToString()
        {
            return MenuName ?? "New Menu";
        }
    }

    public class MenuArgsModel
    {
        [Category("Menu Settings"), DisplayName("Menu Name"), Description("The display name of the menu item.")]
        public required string MenuName { get; set; }

        [Category("Menu Settings"), DisplayName("Menu Action"), Description("Action of the menu item. Can be null if the menu item has children.")]
        public string? MenuAction { get; set; }

        [Category("Menu Settings"), DisplayName("Menu Arguments"), Description("Args of the menu item. Can be null if the menu item has children.")]
        public string? MenuArgs { get; set; }

        [Category("Menu Settings"), DisplayName("Sub Menus"), Description("Child menu items of this menu.")]
        [Editor(typeof(CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<MenuModel> Children { get; set; } = new List<MenuModel>();

        public override string ToString()
        {
            return MenuName ?? "New Menu";
        }
    }

}
