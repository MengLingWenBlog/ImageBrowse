using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing.Drawing2D;
/*using System.Drawing;*/

namespace ImageBrowse
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    //定义Image类型别名
    using MyImage = System.Windows.Controls.Image;

    public class ImageDataSource
    {
        public string namePath { get; set; }
    }

    public partial class MainWindow : Window
    {
        //public List<Bitmap> bmps = new List<Bitmap>();
        //public ObservableCollection<MyImage> imgs = new ObservableCollection<MyImage>();
        public ObservableCollection<ImageDataSource> imgdss = new ObservableCollection<ImageDataSource>();

        public MainWindow()
        {
            InitializeComponent();
            //ImageDataSource imgds = new ImageDataSource();
            //imgds.namePath = @"C:\Users\李帅\Pictures\Saved Pictures\753759.png";
            //imgdss.Add(imgds);

            //ImageDataSource imgds2 = new ImageDataSource();
            //imgds2.namePath = @"C:\Users\李帅\Pictures\Saved Pictures\821016.png";
            //imgdss.Add(imgds2);
            //this.listView.DataContext = imgdss;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //imgs = ImageGetter.GetList(fbd.SelectedPath);
                //foreach (MyImage img in imgs)
                //{
                //    //BitmapSource bs = new BitmapImage(new Uri(@"image file path", UriKind.RelativeOrAbsolute));

                //    this.wrapPanel1.Children.Add(img);
                //}

                //bmps = ImageGetter.GetBitMapList(fbd.SelectedPath);
                //foreach (Bitmap bmp in bmps)
                //{
                //    //Bitmap bmp_temp = KiResizeImage(bmp,20,20);
                //    MyImage img = new MyImage();
                //    img.Source = loadBitmap(bmp);
                //    imgs.Add(img);
                //}
                //this.listView.ItemsSource = imgdss;

                List<string> imgNames = ImageGetter.GetFilePaths(fbd.SelectedPath);
                foreach (string name in imgNames)
                {
                    ImageDataSource imgds = new ImageDataSource();
                    imgds.namePath = name;
                    imgdss.Add(imgds);
                }
                this.listBox.ItemsSource = imgdss;
            }
            else
            {
                System.Windows.MessageBox.Show("请选择一个文件夹");
            }
        }

        private BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                //DeleteObject(ip);
            }

            return bs;
        }


        private Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
