using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ImageBrowse
{
    class ImageGetter
    {
        public ImageGetter()
        {

        }

        public static List<Image> GetList(string folderName)
        {
            IEnumerable<string> fileList = Directory.EnumerateFiles(folderName);
            string[] tokenList = { ".jpg", ".png", ".bmp", ".jpeg", "gif" };
            var imageList = fileList.Where(x => tokenList.Any(x1 => x.EndsWith(x1, StringComparison.InvariantCultureIgnoreCase)));
            var images1 = imageList.Select(x => new Image { Source = new BitmapImage(new Uri(x)) });
            return images1.ToList();
        }

        public static List<System.Drawing.Bitmap> GetBitMapList(string folderName)
        {
            IEnumerable<string> fileList = Directory.EnumerateFiles(folderName);
            string[] imgType = { "*.JPG", "*.PNG", "*.BMP", "*.JPEG", "*.GIF" };
            List<System.Drawing.Bitmap> bmps = new List<System.Drawing.Bitmap>();

            for (int i = 0; i < imgType.Length; i++)
            {
                string[] dirs = Directory.GetFiles(folderName, imgType[i]);
                int j = 0;
                foreach (string dir in dirs)
                {
                    bmps.Add(new System.Drawing.Bitmap(dir));
                }
            }
            return bmps;
        }

        public static List<string> GetFilePaths(string folderName)
        {
            IEnumerable<string> fileList = Directory.EnumerateFiles(folderName);
            string[] imgType = { "*.JPG", "*.PNG", "*.BMP", "*.JPEG", "*.GIF" };
            List<string> filenames = new List<string>();

            for (int i = 0; i < imgType.Length; i++)
            {
                string[] dirs = Directory.GetFiles(folderName, imgType[i]);
                int j = 0;
                filenames.AddRange(dirs);
            }
            return filenames;
        }

        public void getPath(string path, ref List<string> list)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                if (FileIsImage(f))
                {
                    list.Add(f.FullName);//添加文件的路径到列表
                }
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                getPath(d.FullName, ref list);
                //list.Add(d.FullName);//添加文件夹的路径到列表
            }
        }

        private bool FileIsImage(FileInfo f)
        {
            string[] tokenList = { "jpg", "png", "bmp", "jpeg", "gif" };
            foreach (string str in tokenList)
            {
                if (string.Compare(Path.GetExtension(f.FullName), str) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
