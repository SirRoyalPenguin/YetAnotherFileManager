using System.IO;
using System.Windows.Forms;

namespace YetAnotherFileManager
{
    public class CustomListViewItem: ListViewItem
    {
        public DirectoryInfo DirectoryInfo { get; set; }
        public FileInfo FileInfo { get; set; }

        public CustomListViewItemTypeEnum Type { get; set; }

        public string FullPath
        {
            get
            {
                switch(Type)
                {
                    case CustomListViewItemTypeEnum.Directory:
                        return DirectoryInfo.FullName;
                    case CustomListViewItemTypeEnum.File:
                        return FileInfo.FullName;
                    case CustomListViewItemTypeEnum.RemoteDirectory:
                        return "\\\\" + DirectoryInfo.Name;
                    default:
                        return string.Empty;
                }
            }
        }

        public CustomListViewItem(string name, int imageIndex):base(name, imageIndex)
        {
        }
    }
}
