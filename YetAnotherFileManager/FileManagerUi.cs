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
using System.Runtime.InteropServices;

namespace YetAnotherFileManager
{
    public partial class FileManagerUi : Form
    {

        public FileManagerUi()
        {
            this.ResizeEnd += Form_ResizeEnd;
            InitializeComponent();
            InitDirPathTextBoxes();
            InitListViews();
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

        private void InitListViews()
        {
            listViewLeft.MouseDoubleClick += ListView_MouseClick;
            listViewRight.MouseDoubleClick +=ListView_MouseClick;
        }

        private void InitDirPathTextBoxes()
        {
            textBoxDirectoryPathLeft.MouseClick += DirPathTextBox_MouseClick;
            textBoxDirectoryPathRight.MouseClick += DirPathTextBox_MouseClick;

            textBoxDirectoryPathLeft.LostFocus += DirPathTextBox_LostFocus;
            textBoxDirectoryPathRight.LostFocus += DirPathTextBox_LostFocus;

            textBoxDirectoryPathLeft.KeyUp += DirPathTextBox_KeyUp;
            textBoxDirectoryPathRight.KeyUp += DirPathTextBox_KeyUp;
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            var listView = (ListView)sender;
            bool isLeftPanel = listView.Name == "listViewLeft";
            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            CustomListViewItem item = (CustomListViewItem)info.Item;

            if (item != null && item.IsDirectory)
            {
                RefreshListView(isLeftPanel, item.FullPath);
            }
            else
            {
                listView.SelectedItems.Clear();
            }
        }

        private void DirPathTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.ReadOnly = false;
        }
        private void DirPathTextBoxLostFocus(TextBox textBox)
        {
            bool isLeftPanel = textBox.Name == "textBoxDirectoryPathLeft";
            textBox.ReadOnly = true;

            RefreshListView(isLeftPanel, textBox.Text);
        }
        private void DirPathTextBox_LostFocus(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            DirPathTextBoxLostFocus(textBox);
        }

        private void DirPathTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            var textBox = (TextBox)sender;
            DirPathTextBoxLostFocus(textBox);
        }

        private void BtnDisk_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var diskName = (btn).Text;
          
            var parent = (FlowLayoutPanel)btn.Parent;
            bool isLeftPanel = parent.Name == "flowPanelLeftDisks";
            RefreshListView(isLeftPanel, diskName);
        }

        private void RefreshListView(bool isLeftPanel, string dirPath)
        {
            try
            {
                var listView = isLeftPanel ? listViewLeft : listViewRight;
                var pathTextBox = listView.Parent.Controls.OfType<TextBox>().FirstOrDefault();
                if (pathTextBox != null)
                {
                    pathTextBox.Text = dirPath;
                }
                listView.Items.Clear();
                var dirInfo = new DirectoryInfo(dirPath);
                ListViewItem.ListViewSubItem[] subItems;
                CustomListViewItem item = null;
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    item = new CustomListViewItem(dir.Name, 0);
                    item.DirectoryInfo = dir;

                    subItems = new ListViewItem.ListViewSubItem[]
                              {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                    item.SubItems.AddRange(subItems);
                    listView.Items.Add(item);
                }
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    item = new CustomListViewItem(file.Name, 1);
                    item.FileInfo = file;

                    subItems = new ListViewItem.ListViewSubItem[]
                              { new ListViewItem.ListViewSubItem(item, "File"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                    item.SubItems.AddRange(subItems);
                    listView.Items.Add(item);
                }

                ResizeColumnHeaders();
            }
            catch
            {
                MessageBox.Show(string.Format("unable to access {0}", dirPath));
            }

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
