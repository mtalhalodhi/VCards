using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VCards.Cards
{
    /// <summary>
    /// Interaction logic for AlarmCard.xaml
    /// </summary>
    public partial class AlarmCard : Window, ICard<AlarmCard.Properties>
    {
        public static Timer Timer = new Timer(30000);

        public class Properties : ICardProperties
        {
            public DateTime Time { get; set; }
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
                    Time = Clock.Time,
                    Left = Left,
                    Height = Height,
                    Width = Width,
                    Top = Top
                };
            }
            set
            {
                Clock.Time = value.Time;
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

        Dispatcher dispatcher;

        public AlarmCard()
        {
            InitializeComponent();

            dispatcher = Dispatcher.CurrentDispatcher;

            Remove.Click += Remove_Click;

            Timer.Elapsed += Timer_Elapsed;

            if(Timer.Enabled != true)
            {
                Timer.AutoReset = true;
                Timer.Enabled = true;
                Timer.Start();
            }
            MainWindow.OnOpacityChanged += o => VisualOpacity = o;

            VisualOpacity = MainWindow.ThemeOpacity;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            dispatcher.InvokeAsync(() =>
            {
                if (Clock.Time.Hour == DateTime.Now.Hour && Clock.Time.Minute == DateTime.Now.Minute)
                {
                    MessageBox.Show("Tick Tock Tick Tock");
                }
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); } catch { }
        }
    }
}
