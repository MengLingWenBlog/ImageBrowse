using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

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
                var imgs = new ImageGetter().GetList(fbd.SelectedPath);
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

        public ImageList GetList(string folderName)
        {
            var fileList = Directory.EnumerateFiles(folderName);
            string[] tokenList = { ".jpg", ".png", ".bmp", ".jpeg", "gif" };
            var imageList = fileList.Where(x => tokenList.Any(x1 => x.EndsWith(x1, StringComparison.InvariantCultureIgnoreCase)));
            var images1 = imageList.Select(x => new Image{ Source = new BitmapImage(new Uri(x)) });
            return images1.ToList();
        }
    }
}
