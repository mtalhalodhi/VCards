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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VCards.Controls
{
    /// <summary>
    /// Interaction logic for CardHeader.xaml
    /// </summary>
    public partial class CardHeader : UserControl
    {
        public event RoutedEventHandler OnCloseButtonPressed
        {
            add
            {
                Close.Click += value;
            }
            remove
            {
                Close.Click -= value;
            }
        }

        public string Title
        {
            get
            {
                return TitleBox.Text;
            }
            set
            {
                TitleBox.Text = value;
            }
        }

        public CardHeader()
        {
            InitializeComponent();
        }
    }
}
