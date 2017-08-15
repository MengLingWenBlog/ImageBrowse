using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
/*using System.Drawing;*/

namespace ImageBrowse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.MessageBox.Show("文件夹打开成功");
                ImageGetter imgGetter = new ImageGetter();
                List <Image> imgs = imgGetter.GetList(fbd.SelectedPath);
                //new PanelManage().AddRange(wrapPanel1, imgs);
                foreach (Image img in imgs)
                {
                    //System.Drawing.Bitmap img_temp = new System.Drawing.Bitmap(img, 20, 20);
                    //this.wrapPanel1.Children.Add(img_temp);
                    this.wrapPanel1.Children.Add(img);
                }

                List<string> list = new List<string>();
                imgGetter.getPath(fbd.SelectedPath, ref list);
                System.Windows.MessageBox.Show(list.ToString());
            }
            else
            {
                System.Windows.MessageBox.Show("请选择一个文件夹");
            }
            //var fileList = Dirctory.EnumerateFile();
        }

        
    }

    class ImageGetter
    {
        public ImageGetter()
        {

        }

        public List<Image> GetList(string folderName)
        {
            IEnumerable<string> fileList = Directory.EnumerateFiles(folderName);
            string[] tokenList = { ".jpg", ".png", ".bmp", ".jpeg", "gif" };
            var imageList = fileList.Where(x => tokenList.Any(x1 => x.EndsWith(x1, StringComparison.InvariantCultureIgnoreCase)));
            var images1 = imageList.Select(x => new Image { Source = new BitmapImage(new Uri(x)) });
            return images1.ToList();

            //BitmapFactory.Options options = new BitmapFactory.Options（）;

            //options.inSampleSize = 2;

            //Bitmap img = BitmapFactory.decodeFile（"/sdcard/1.png"， options）;

            //Bitmap bm = new Bitmap(fileList[0]);

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
                getPath(d.FullName,ref list);
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
