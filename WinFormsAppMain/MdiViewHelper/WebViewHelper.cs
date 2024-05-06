using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace MdiViewHelper
{
    // Revised static helper method ensuring NavigationCompletedHandler is removed on completion regardless of success or failure
    public static class WebViewHelper
    {
        public static async Task<string?> LoadUrlAndGetHtmlAsync(CoreWebView2 coreWebView, string url)
        {
            TaskCompletionSource<bool> navigationCompletedTcs = new TaskCompletionSource<bool>();

            void NavigationCompletedHandler(object? sender, CoreWebView2NavigationCompletedEventArgs e)
            {
                coreWebView.NavigationCompleted -= NavigationCompletedHandler;
                navigationCompletedTcs.SetResult(e.IsSuccess);
            }

            coreWebView.NavigationCompleted += NavigationCompletedHandler;
            coreWebView.Navigate(new Uri(url).ToString());
            await navigationCompletedTcs.Task;

            if (navigationCompletedTcs.Task.Result)
            {
                return await coreWebView.ExecuteScriptAsync("document.documentElement.outerHTML;");
            }

            return null; // Or an appropriate default value or error handling as needed
        }
    }

}
