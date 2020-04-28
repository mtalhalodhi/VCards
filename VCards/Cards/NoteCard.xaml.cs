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
    /// Interaction logic for NoteCard.xaml
    /// </summary>
    public partial class NoteCard : Window, ICard<NoteCard.Properties>
    {
        public class Properties : ICardProperties
        {
            public string Title { get; set; }
            public string Text { get; set; }
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
                    Title = CardHeader.Title,
                    Text = NoteText.Text,
                    Left = Left,
                    Height = Height,
                    Width = Width,
                    Top = Top
                };
            }
            set
            {
                CardHeader.Title = value.Title;
                NoteText.Text = value.Text;
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

        public NoteCard()
        {
            InitializeComponent();

            CardHeader.OnCloseButtonPressed += Remove_Click;

            MainWindow.OnOpacityChanged += o => VisualOpacity = o;

            VisualOpacity = MainWindow.ThemeOpacity;
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CardHeader_OnCloseButtonPressed(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
