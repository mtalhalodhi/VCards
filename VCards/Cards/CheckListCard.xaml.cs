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
    /// Interaction logic for CheckListCard.xaml
    /// </summary>
    public partial class CheckListCard : Window, ICard<CheckListCard.Properties>
    {
        public class CheckItemTuple
        {
            public bool Checked { get; set; }
            public string Text { get; set; }
        }

        public class Properties : ICardProperties
        {
            public string Title { get; set; }
            public List<CheckItemTuple> Items { get; set; }
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
                    Items = Items,
                    Left = Left,
                    Height = Height,
                    Width = Width,
                    Top = Top
                };
            }
            set
            {
                CardHeader.Title = value.Title;
                Items = value.Items;
                Left = value.Left;
                Height = value.Height;
                Width = value.Width;
                Top = value.Top;

                CheckListBox.ItemsSource = null;
                CheckListBox.ItemsSource = items;
            }
        }

        public event RoutedEventHandler OnClosedCalled;
        
        public List<CheckItemTuple> items = new List<CheckItemTuple>();
        public List<CheckItemTuple> Items { get => items; set => items = value; }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            OnClosedCalled(this, new RoutedEventArgs());
        }

        public CheckListCard()
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

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new CheckItemTuple() { Checked = false, Text = "" });
            CheckListBox.ItemsSource = null;
            CheckListBox.ItemsSource = items;
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                items.RemoveAt(CheckListBox.SelectedIndex);
                CheckListBox.ItemsSource = null;
                CheckListBox.ItemsSource = items;
            }
            catch
            {
                
            }
        }
    }
}
