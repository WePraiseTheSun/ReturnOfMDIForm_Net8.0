using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace AllChatAI
{
    public partial class Form_WebView : Form
    {
        public string URL { get; set; }

        public Form_WebView()
        {
            InitializeComponent();
            this.Load += async(s, e) => {

                try
                {
                    // Initialize the WebView2 environment
                    var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TiledEdgeBrowser";
                    var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                    await WebView_Display.EnsureCoreWebView2Async(env);
                }
                catch (Exception ex)
                {

                }
           

                CoreWebView2EnvironmentOptions envOptions = new CoreWebView2EnvironmentOptions();
                envOptions.TargetCompatibleBrowserVersion = "86.0";
                envOptions.AdditionalBrowserArguments = "--silent --install-all-edge-runtimes --force --no-progress-bar";

                //WebView_Display.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                //WebView_Display.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                //WebView_Display.SourceChanged += WebView_Display_SourceChanged;

                //Nagivate(this.Text);
   
           //     WebView_Display.Source = new Uri(URL);

            };
        }
    }
}
