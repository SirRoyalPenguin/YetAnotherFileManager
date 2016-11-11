using System.IO;
using System.Windows.Forms;

namespace YetAnotherFileManager
{
    public class CustomListViewItem: ListViewItem
    {
        public DirectoryInfo DirectoryInfo { get; set; }
        public FileInfo FileInfo { get; set; }

        public string FullPath
        {
            get
            {
                return IsDirectory ? DirectoryInfo.FullName : FileInfo.FullName;
            }
        }
        public bool IsDirectory
        {
            get
            {
                return DirectoryInfo != null;
            }
        }

        public CustomListViewItem(string name, int imageIndex):base(name, imageIndex)
        {
        }
    }
}
