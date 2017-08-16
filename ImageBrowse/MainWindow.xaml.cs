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

            ////ImageDataSource imgds2 = new ImageDataSource();
            //imgds2.namePath = @"C:\Users\李帅\Pictures\Saved Pictures\821016.png";
            //imgdss.Add(imgds2);
            //this.listView.DataContext = imgdss;
        }
        

        private void ClickLoadMaginButton(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //loadBitmap(fbd.SelectedPath);
                this.loadMaginText.Text = fbd.SelectedPath;
            }
            else
            {
                System.Windows.MessageBox.Show("请选择一个文件夹");
            }
        }



        private void loadBitmap(string path)
        {
            //路径无效则直接返回
            if (!System.IO.Directory.Exists(path))
            {
                return;
            }
            //目录下无图片文件则直接返回
            List<string> imgNames = ImageGetter.GetFilePaths(path);
            if (imgNames.Count <= 0)
            {
                return;
            }
            this.loadMaginText.Text = path;
            int countSuccess = 0;
            int countFaild = 0;
            foreach (string name in imgNames)
            {
                bool inImgdss = false;
                foreach (var imgds_temp in imgdss)
                {
                    if (name.Equals(imgds_temp.namePath))
                    {
                        inImgdss = true;
                        break;
                    }
                }
                //如果该图片不在原先的图片列表中，则追加
                if (!inImgdss)
                {
                    countSuccess++;
                    ImageDataSource imgds = new ImageDataSource();
                    imgds.namePath = name;
                    imgdss.Add(imgds);
                }
                else
                {
                    countFaild++;
                }
            }
            this.listBox.ItemsSource = imgdss;

            string str = "载入成功图片： " + countSuccess + "个";
            str += countFaild <= 0 ? "" : "\n重复图片：" + countFaild + "个";
            System.Windows.MessageBox.Show(str);
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

        private void ClickLeftButton(object sender, RoutedEventArgs e)
        {
            if(this.listBox.SelectedIndex>0)
                this.listBox.SelectedIndex--;
        }

        private void ClickRightButton(object sender, RoutedEventArgs e)
        {
            //此处++运算符被重载，自动检测不超出长度才自增
            this.listBox.SelectedIndex++;
        }

        private void loadMaginText_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadBitmap((sender as System.Windows.Controls.TextBox).Text);
        }
    }
}
