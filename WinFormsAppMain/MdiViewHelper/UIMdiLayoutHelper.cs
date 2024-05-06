using System;
using System.Windows.Forms;

namespace MdiViewHelper
{
    public static class UIMdiLayoutHelper
    {
        // Define a delegate that takes a Form as a parameter
        public delegate void LayoutDelegate(Form mdiParent);

        public static void ManageMdiLayout(Form mdiParent, LayoutDelegate? layoutFunction = null)
        {
            // Define the default layout function if none is provided
            layoutFunction ??= (parent) => parent.LayoutMdi(MdiLayout.TileVertical);

            EventHandler manageLayout = (sender, e) =>
            {
                bool anyMaximized = false;
                foreach (Form child in mdiParent.MdiChildren)
                {
                    if (child.WindowState == FormWindowState.Maximized)
                    {
                        anyMaximized = true;
                        break;
                    }
                }

                if (!anyMaximized)
                {
                    layoutFunction(mdiParent);
                }
            };

            mdiParent.Load += manageLayout;
            mdiParent.Resize += manageLayout;

            foreach (Form child in mdiParent.MdiChildren)
            {
                AttachResizeEvent(child, layoutFunction);
            }

            mdiParent.MdiChildActivate += (sender, e) =>
            {
                if (mdiParent.ActiveMdiChild != null && mdiParent.ActiveMdiChild.Tag == null)
                {
                    AttachResizeEvent(mdiParent.ActiveMdiChild, layoutFunction);
                    mdiParent.ActiveMdiChild.Tag = "NormalOrMinimized";
                    layoutFunction(mdiParent);
                }
            };
        }

        private static void AttachResizeEvent(Form child, LayoutDelegate layoutFunction)
        {
            child.Resize += (sender, e) =>
            {
                //Form? childForm = sender as Form;
                //if (childForm != null)
                //{
                //    if (childForm.Tag?.ToString() == "Maximized" && childForm.WindowState != FormWindowState.Maximized)
                //    {
                //        childForm.Tag = "NormalOrMinimized";
                //        layoutFunction(childForm.MdiParent);  // Use delegate to tile
                //    }
                //    else if (childForm.WindowState == FormWindowState.Maximized)
                //    {
                //        childForm.Tag = "Maximized";
                //    }
                //    else if (childForm.WindowState == FormWindowState.Minimized || childForm.WindowState == FormWindowState.Normal)
                //    {
                //        childForm.Tag = "NormalOrMinimized";
                //        layoutFunction(childForm.MdiParent);
                //    }
            
                //}
            };

            child.FormClosed += (sender, e) =>
            {
                Form? childForm = sender as Form;
                Form? parent = childForm.MdiParent;
                parent.BeginInvoke(() => layoutFunction(parent));
            };
        }
    }
}
