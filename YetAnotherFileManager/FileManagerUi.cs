using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace YetAnotherFileManager
{
    public partial class FileManagerUi : Form
    {
        private Regex RemoteFolderRootRegex;
        public FileManagerUi()
        {
            RemoteFolderRootRegex = new Regex(@"^\\\\[^\\]+$");

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
            Func<string,EventHandler, Button> createButton = delegate (string name, EventHandler click)
            {
                var btn = new Button
                {
                    Text = name,
                    Font = btnFont,
                    Height = 24,
                    Width = 32
                };
                btn.Click += click;
                return btn;
            };

            foreach(var drive in drives)
            {
                flowPanelLeftDisks.Controls.Add(createButton(drive.Name, BtnDisk_Click));
                flowPanelRightDisks.Controls.Add(createButton(drive.Name, BtnDisk_Click));
            }

            flowPanelLeftDisks.Controls.Add(createButton("\\\\", BtnRemoteDisk_Click));
            flowPanelRightDisks.Controls.Add(createButton("\\\\", BtnRemoteDisk_Click));
        }

        private void InitListViews()
        {
            listViewLeft.MouseDoubleClick += ListView_MouseDoubleClick;
            listViewRight.MouseDoubleClick +=ListView_MouseDoubleClick;

            listViewLeft.KeyDown += ListView_KeyDown;
            listViewRight.KeyDown += ListView_KeyDown;
        }
        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var listView = (ListView)sender;
            bool isLeftPanel = listView.Name == "listViewLeft";

            var sourceListView = isLeftPanel ? listViewLeft : listViewRight;
            var destPath = isLeftPanel ? textBoxDirectoryPathRight.Text : textBoxDirectoryPathLeft.Text;
            var selectedItem = (CustomListViewItem)sourceListView.SelectedItems[0];

            if(selectedItem.Type != CustomListViewItemTypeEnum.File)
            {
                MessageBox.Show("Alas, theese features not implemented for directories yet");
                return;
            }

            var fileName = selectedItem.FileInfo.Name;

            if (e.KeyCode == Keys.F5)
            {
                try
                {
                    File.Copy(selectedItem.FullPath, destPath + "\\" + fileName);
                    RefreshListView(!isLeftPanel, destPath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to copy");
                }
            }
            if(e.KeyCode == Keys.Delete)
            {
                var dialogResult = MessageBox.Show(string.Format("Are you sure you want to delete {0} forever?", fileName), "Think twice", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(selectedItem.FullPath);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Unable to delete");
                    }

                    RefreshListView(isLeftPanel);
                    RefreshListView(!isLeftPanel);
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
        }
        private void InitDirPathTextBoxes()
        {
            textBoxDirectoryPathLeft.MouseDoubleClick += DirPathTextBox_MouseDoubleClick;
            textBoxDirectoryPathRight.MouseDoubleClick += DirPathTextBox_MouseDoubleClick;

            textBoxDirectoryPathLeft.LostFocus += DirPathTextBox_LostFocus;
            textBoxDirectoryPathRight.LostFocus += DirPathTextBox_LostFocus;

            textBoxDirectoryPathLeft.KeyUp += DirPathTextBox_KeyUp;
            textBoxDirectoryPathRight.KeyUp += DirPathTextBox_KeyUp;
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = (ListView)sender;
            bool isLeftPanel = listView.Name == "listViewLeft";
            ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
            CustomListViewItem item = (CustomListViewItem)info.Item;

            if (item != null && item.Type == CustomListViewItemTypeEnum.Directory || item.Type == CustomListViewItemTypeEnum.RemoteDirectory)
            {
                RefreshListView(isLeftPanel, item.FullPath);
            }
            else
            {
                Process proc = new Process();
                proc.StartInfo.FileName = item.FileInfo.FullName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }

        private void DirPathTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.ReadOnly = false;
        }
        private void DirPathTextBoxLostFocus(TextBox textBox)
        {
            textBox.ReadOnly = true;

            if (string.IsNullOrEmpty(textBox.Text))
                return;

            bool isLeftPanel = textBox.Name == "textBoxDirectoryPathLeft";

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

        private void BtnRemoteDisk_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var diskName = (btn).Text;

            var parent = (FlowLayoutPanel)btn.Parent;
            bool isLeftPanel = parent.Name == "flowPanelLeftDisks";
            RefreshListView(isLeftPanel, diskName);
        }

        private void RefreshListView(bool isLeftPanel, string dirPath="")
        {
            try
            {
                var listView = isLeftPanel ? listViewLeft : listViewRight;
                if (string.IsNullOrEmpty(dirPath))
                {
                    dirPath = isLeftPanel ? textBoxDirectoryPathLeft.Text : textBoxDirectoryPathRight.Text;
                }

                if (string.IsNullOrEmpty(dirPath))
                    return;

                var pathTextBox = listView.Parent.Controls.OfType<TextBox>().FirstOrDefault();
                if (pathTextBox != null)
                {
                    if (dirPath.StartsWith("\\") && !dirPath.Equals("\\\\"))
                        dirPath = dirPath.TrimEnd('\\');
                    pathTextBox.Text = dirPath;
                }

                listView.Items.Clear();

                if(dirPath.Equals("\\\\"))
                {
                    ListRemoteServers(listView);
                }
                else if (RemoteFolderRootRegex.IsMatch(dirPath))
                {
                    ListRemoteFiles(listView, dirPath);
                }
                else
                {
                    var dirInfo = new DirectoryInfo(dirPath);
                    ListFiles(listView, dirInfo);
                }

                ResizeColumnHeaders();
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Format("unable to access {0}", dirPath));
                this.textBoxDirectoryPathLeft.Text = this.textBoxDirectoryPathRight.Text = string.Empty;
                this.listViewLeft.Focus();
            }

        }

        private void ListRemoteServers(ListView listView)
        {
            DirectoryEntry root = new DirectoryEntry("WinNT:");

            foreach (DirectoryEntry computers in root.Children)
            {
                foreach (DirectoryEntry computer in computers.Children)
                {
                   
                    if (computer == null || computer.SchemaClassName != "Computer")
                        continue;

                    var di = new DirectoryInfo("\\"+computer.Name);
                    var item = CreateListViewItem(CustomListViewItemTypeEnum.RemoteDirectory, di, null);
                    listView.Items.Add(item);
                }
            }
        }
    
        private void ListFiles(ListView listView, DirectoryInfo dirInfo, bool listRoot=false)
        {
            CustomListViewItem item = null;

            if (listRoot)
            {
                item = CreateListViewItem(CustomListViewItemTypeEnum.Directory, dirInfo, null);
                listView.Items.Add(item);
            }
            else
            {
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {

                    item = CreateListViewItem(CustomListViewItemTypeEnum.Directory, dir, null);
                    listView.Items.Add(item);
                }
            }
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                item = CreateListViewItem(CustomListViewItemTypeEnum.File,null, file);
                listView.Items.Add(item);
            }
        }

        private void ListRemoteFiles(ListView listView, string path)
        {
            var shi = ShareCollection.GetShares(path);
            if (shi != null)
            {
                foreach (Share si in shi)
                {
                    if (!si.IsFileSystem || si.ShareType != ShareType.Disk)
                    {
                        continue;
                    }

                    DirectoryInfo d = si.Root;
                    ListFiles(listView, d, true);

                }
            }
        }

        private CustomListViewItem CreateListViewItem(CustomListViewItemTypeEnum itemType, DirectoryInfo dirInfo, FileInfo fInfo)
        {
            bool isDir = itemType != CustomListViewItemTypeEnum.File;
            string name = string.Empty;
            string lastAccessTime = string.Empty;

            switch(itemType)
            {
                case CustomListViewItemTypeEnum.Directory:
                    name = dirInfo.Name;
                    lastAccessTime = dirInfo.LastAccessTime.ToShortDateString();
                    break;
                case CustomListViewItemTypeEnum.File:
                    name = fInfo.Name;
                    lastAccessTime = fInfo.LastAccessTime.ToShortDateString();
                    break;
                case CustomListViewItemTypeEnum.RemoteDirectory:
                    name = "\\\\" + dirInfo.Name;
                    break;
                default:
                    name = string.Empty;
                    break;
            }

            var item = new CustomListViewItem(name, (int)itemType);
            item.DirectoryInfo = dirInfo;
            item.FileInfo = fInfo;
            item.Type = itemType;

            var subItems = new ListViewItem.ListViewSubItem[]
            {
                new ListViewItem.ListViewSubItem(item, itemType.ToString()),
                new ListViewItem.ListViewSubItem(item, lastAccessTime)
            };
            item.SubItems.AddRange(subItems);
            return item;
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
