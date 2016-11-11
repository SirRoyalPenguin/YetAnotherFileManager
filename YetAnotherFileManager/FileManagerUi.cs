using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YetAnotherFileManager
{
    public partial class FileManagerUi : Form
    {
        public FileManagerUi()
        {
            this.ResizeEnd += Form_ResizeEnd;
            InitializeComponent();
            InitDiskButtons();
        }

        private void InitDiskButtons()
        {
            var drives = DriveInfo.GetDrives();
            var btnFont = new Font("Arial", 8);
            Func<string, Button> createButton = delegate (string name)
            {
                var btn = new Button
                {
                    Text = name,
                    Font = btnFont,
                    Height = 24,
                    Width = 32
                };
                btn.Click += BtnDisk_Click;
                return btn;
            };

            foreach(var drive in drives)
            {
                flowPanelLeftDisks.Controls.Add(createButton(drive.Name));
                flowPanelRightDisks.Controls.Add(createButton(drive.Name));
            }
        }

        private void BtnDisk_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var diskName = (btn).Text;
            try
            {
                var parent = (FlowLayoutPanel)btn.Parent;
                bool isLeftPanel = parent.Name == "flowPanelLeftDisks";
                RefreshListView(isLeftPanel, diskName);
            }
            catch
            {
                MessageBox.Show(string.Format("unable to access disk {0}", diskName));
            }
        }
        private void RefreshListView(bool isLeftPanel, string dirPath)
        {
            var listView = isLeftPanel ? listViewLeft : listViewRight;
            listView.Items.Clear();
            var dirInfo = new DirectoryInfo(dirPath);
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView.Items.Add(item);
            }
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "File"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                listView.Items.Add(item);
            }

            ResizeColumnHeaders();

        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            ResizeColumnHeaders();
        }

        private void ResizeColumnHeaders()
        {
            listViewLeft.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewRight.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
