using MdiViewHelper;

namespace AllChatAI
{
    public partial class Form_Main : Form
    {
        private bool isInvoked = false;

        public Form_Main()
        {
            InitializeComponent();

            #region Do Not Edit

            UIMdiLayoutHelper.ManageMdiLayout(this, _ =>
            {

                if (!isInvoked)
                {
                    isInvoked = true;

                    //This can be disabled for better performance but window will be fixed to its position
                    Form[] children = MdiChildren.OrderBy(f => f.Handle).ToArray();
                    foreach (var c in children)
                    {
                        c.BringToFront();
                    }
                    LayoutMdi(MdiLayout.TileVertical);
                    isInvoked = false;
                }
            });
            GlobalVariable.InitialiseSettingModel();
            Controls.Add(MainMenuStrip = new MenuStrip());
            InitializeMenuStrip(MainMenuStrip);
            #endregion

            this.Load += (s, e) =>
            {
                OpenURL("gemini.google.com");
                OpenURL("gemini.google.com");
                OpenURL("gemini.google.com");




            };

        }
        public void InitializeMenuStrip(MenuStrip menuStrip)
        {
            #region Do Not Edit
            menuStrip.Items.Clear();
            //System Menu
            menuStrip.Items.Add(new ToolStripMenuItem("&File")
            {
                DropDownItems =
                {
                   new ToolStripMenuItem("&Configuration", null, (s, e) =>
                        {
                            var form = new Form
                            {
                                Text = "Configuration",
                                Size = new Size(400, 300),
                                StartPosition = FormStartPosition.CenterParent,
                                FormBorderStyle = FormBorderStyle.FixedDialog,
                                MaximizeBox = false,
                                MinimizeBox = false,
                                Owner = this,
                                Controls =
                                {
                                     new PropertyGrid
                                    {
                                        Dock = DockStyle.Fill,
                                        SelectedObject = GlobalVariable.ConfigurationModel,
                                        PropertySort = PropertySort.Alphabetical
                                    }
                                }
                            };

                            form.FormClosing += (sender, args) =>
                            {
                                ClassToJsonHelper.SaveToFile(GlobalVariable.DefaultSettingFile, GlobalVariable.ConfigurationModel);
                                GlobalVariable.ConfigurationModel = ClassToJsonHelper.LoadFromFile<ConfigurationModel>(GlobalVariable.DefaultSettingFile);

                                BeginInvoke(() =>
                                {
                                    GlobalVariable.InitialiseSettingModel();
                                    InitializeMenuStrip(MainMenuStrip);
                                });
                            };
                            form.ShowDialog();
                        }),
                   new ToolStripMenuItem("&Restore Default Settings", null, (s, e) =>
                   {
                       var result = MessageBox.Show(this, "Are you sure you want to restore to default settings?", "Confirm Delete", MessageBoxButtons.YesNo);
                       if (result == DialogResult.Yes)
                       {
                           File.Delete(GlobalVariable.DefaultSettingFile);
                              BeginInvoke(() =>
                                {
                                    GlobalVariable.InitialiseSettingModel();
                                    InitializeMenuStrip(MainMenuStrip);
                                });
                       }
                   }) { ShortcutKeys = Keys.Control | Keys.F1 },
                   new ToolStripMenuItem("E&xit", null, (s, e) => Application.Exit()) { ShortcutKeys = Keys.Alt | Keys.F4 }
                }
            });

            menuStrip.Items.Add(new ToolStripMenuItem("&Help")
            {
                DropDownItems =
                {
                    new ToolStripMenuItem("&About", null, (s, e) =>
                    {
                     var form = new Form
                    {
                        Text = "About",
                        Size = new Size(300, 200),
                        StartPosition = FormStartPosition.CenterParent,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        MaximizeBox = false,
                        MinimizeBox = false,
                        Owner = this,
                        Controls =
                        {
                            new TableLayoutPanel
                            {
                                Dock = DockStyle.Fill,
                                RowStyles = { new RowStyle(SizeType.Percent, 50F), new RowStyle(SizeType.Percent, 50F) },
                                Padding = new Padding(10),
                                Controls =
                                {
                                    new Label
                                    {
                                        Text = "Version 1.0.0",
                                        Dock = DockStyle.Fill,
                                        TextAlign = ContentAlignment.MiddleCenter
                                    },
                                    new LinkLabel
                                    {
                                        Text = "Visit Homepage",
                                        Dock = DockStyle.Fill,
                                        TextAlign = ContentAlignment.MiddleCenter,
                                        LinkBehavior = LinkBehavior.HoverUnderline
                                    }
                                }
                            }
                        }
                    };

                    var linkLabel = (LinkLabel)form.Controls[0].Controls[1];
                    linkLabel.Links.Add(new LinkLabel.Link(0, linkLabel.Text.Length, "https://www.example.com"));
                    linkLabel.LinkClicked += (sender, args) =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = args.Link.LinkData.ToString(),
                            UseShellExecute = true
                        });
                    };

                    form.ShowDialog();
                    }),
                }
            });
            #endregion
            //Bookmark Menu, add more here
            menuStrip.Items.Insert(1, UIMenuUtility.CreateMenu(GlobalVariable.ConfigurationModel.Menu,
                url => (s, e) => new Form_WebView { MdiParent = this, URL = url }.Show()));
        }
        public void OpenURL(string url)
        {
            if (url == null) return;

            var childForm = new Form_WebView { MdiParent = this };
            childForm.Text = url;

            //   childForm.UpdateURLEvent += ChildForm_CustomEvent;
            childForm.Show();
        }
    }
}
