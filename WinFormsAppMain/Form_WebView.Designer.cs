namespace AllChatAI
{
    partial class Form_WebView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WebView_Display = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)WebView_Display).BeginInit();
            SuspendLayout();
            // 
            // WebView_Display
            // 
            WebView_Display.AllowExternalDrop = true;
            WebView_Display.CreationProperties = null;
            WebView_Display.DefaultBackgroundColor = Color.White;
            WebView_Display.Dock = DockStyle.Fill;
            WebView_Display.Location = new Point(0, 0);
            WebView_Display.Name = "WebView_Display";
            WebView_Display.Size = new Size(800, 450);
            WebView_Display.TabIndex = 0;
            WebView_Display.ZoomFactor = 1D;
            // 
            // Form_WebView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(WebView_Display);
            Name = "Form_WebView";
            Text = "Untitled";
            ((System.ComponentModel.ISupportInitialize)WebView_Display).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebView_Display;
    }
}