using HelixToolkit.Wpf;
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
    /// Interaction logic for ModelCard.xaml
    /// </summary>
    public partial class ModelCard : Window, ICard<ModelCard.Properties>
    {
        public class Properties : ICardProperties
        {
            public string ModelSource { get; set; }
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
                    ModelSource = ModelSource,
                    Left = Left,
                    Height = Height,
                    Width = Width,
                    Top = Top
                };
            }
            set
            {
                ModelSource = value.ModelSource;
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

        public ModelCard()
        {
            InitializeComponent();

            Remove.Click += Remove_Click;
            MainWindow.OnOpacityChanged += o => VisualOpacity = o;

            VisualOpacity = MainWindow.ThemeOpacity;
        }

        string modelSource = "";
        public string ModelSource
        {
            get
            {
                return modelSource;
            }
            set
            {
                modelSource = value;
                try
                {
                    ModelImporter importer = new ModelImporter();
                    Model.Content = importer.Load(value);
                }
                catch
                {

                }
            }
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            Flipper.IsFlipped = !Flipper.IsFlipped;
            Viewport3D.Visibility = Flipper.IsFlipped ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OpenPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            var result = open.ShowDialog();

            if (result.HasValue && result.Value)
            {
                try
                {
                    ModelSource = open.FileName;
                }
                catch
                {

                }

                Flipper.IsFlipped = !Flipper.IsFlipped;
                Viewport3D.Visibility = Flipper.IsFlipped ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
