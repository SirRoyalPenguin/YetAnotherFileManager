﻿using System;
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

            flowPanelLeftDisks.Controls.Add(createButton("\\", BtnRemoteDisk_Click));
            flowPanelRightDisks.Controls.Add(createButton("\\", BtnRemoteDisk_Click));
        }

        private void InitListViews()
        {
            listViewLeft.MouseDoubleClick += ListView_MouseClick;
            listViewRight.MouseDoubleClick +=ListView_MouseClick;
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

                if (!dirPath.StartsWith("\\"))
                {
                    var dirInfo = new DirectoryInfo(dirPath);
                    ListFiles(listView, dirInfo);
                }
                else
                {
                    ListRemoteFiles(listView, dirPath);
                }

                ResizeColumnHeaders();
            }
            catch
            {
                MessageBox.Show(string.Format("unable to access {0}", dirPath));
            }

        }

        private void ListFiles(ListView listView, DirectoryInfo dirInfo)
        {
            CustomListViewItem item = null;

            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
              
                item = CreateListViewItem(CustomListViewItemTypeEnum.Directory, dir, null);
                listView.Items.Add(item);
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
                    ListFiles(listView, d);

                }
            }
        }

        private CustomListViewItem CreateListViewItem(CustomListViewItemTypeEnum itemType, DirectoryInfo dirInfo, FileInfo fInfo)
        {
            bool isDir = itemType != CustomListViewItemTypeEnum.File;
            var name = isDir ? dirInfo.Name : fInfo.Name;
            var lastAccessTime = isDir ? dirInfo.LastAccessTime : fInfo.LastAccessTime;

            var item = new CustomListViewItem(name, (int)itemType);
            item.DirectoryInfo = dirInfo;
            item.FileInfo = fInfo;

            var subItems = new ListViewItem.ListViewSubItem[]
                      {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                lastAccessTime.ToShortDateString())};
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
