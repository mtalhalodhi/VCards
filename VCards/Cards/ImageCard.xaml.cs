using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VCards.Cards
{
    /// <summary>
    /// Interaction logic for ImageCard.xaml
    /// </summary>
    public partial class ImageCard : Window, ICard<ImageCard.Properties>
    {
        public class Properties : ICardProperties
        {
            public string ImageSource { get; set; }
            public double Left { get; set; }
            public double Top { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }

        public Properties CardProperties
        {
            get
            {
                return new Properties
                {
                    ImageSource = ImageSource,
                    Left = Left,
                    Height = Height,
                    Width = Width,
                    Top = Top
                };
            }
            set
            {
                ImageSource = value.ImageSource;
                Left = value.Left;
                Height = value.Height;
                Width = value.Width;
                Top = value.Top;
            }
        }

        public event RoutedEventHandler OnClosedCalled;

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            OnClosedCalled(this, new RoutedEventArgs());
        }

        public ImageCard()
        {
            InitializeComponent();

            Remove.Click += Remove_Click;
            MainWindow.OnOpacityChanged += o => VisualOpacity = o;

            VisualOpacity = MainWindow.ThemeOpacity;
        }

        string imageSource = "";
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                try
                {
                    Photo.Source = new BitmapImage(new Uri(value));
                }
                catch
                {

                }
            }
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            Flipper.IsFlipped = !Flipper.IsFlipped;
        }

        private void OpenPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            var result = open.ShowDialog();

            if(result.HasValue && result.Value)
            {
                ImageSource = open.FileName;
                Flipper.IsFlipped = !Flipper.IsFlipped;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
