using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppMain.SharedClasses
{
    // Extension method to simplify invoking async methods on the UI thread
    public static class ControlExtensions
    {
        public static Task InvokeAsync(this Control control, Func<Task> func)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke(func);
            }
            else
            {
                return func();
            }
        }

        public static void InvokeIfNeeded(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

}
