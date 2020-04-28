using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VCards.Cards;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;

namespace VCards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Theme
        {
            public bool NightMode { get; set; }
            public double Opacity { get; set; }
            public Palette Pallete { get; set; }
        }

        public delegate void OpacityChangedNotifier(double opacity);

        List<ImageCard> ImageCards = new List<ImageCard>();
        List<ModelCard> ModelCards = new List<ModelCard>();
        List<NoteCard> NoteCards = new List<NoteCard>();
        List<AlarmCard> AlarmCards = new List<AlarmCard>();
        List<CheckListCard> CheckListCards = new List<CheckListCard>();

        WinForms.NotifyIcon Notifier = new WinForms.NotifyIcon();

        public static OpacityChangedNotifier OnOpacityChanged;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                ImageCards = JsonConvert.DeserializeObject<List<ImageCard.Properties>>(File.ReadAllText("Images.json")).Select(p => new ImageCard() { CardProperties = p }).ToList();
            }
            catch
            {
                ImageCards = new List<ImageCard>();
            }
            try
            {
                ModelCards = JsonConvert.DeserializeObject<List<ModelCard.Properties>>(File.ReadAllText("Models.json")).Select(p => new ModelCard() { CardProperties = p }).ToList();
            }
            catch
            {
                ModelCards = new List<ModelCard>();
            }
            try
            {
                NoteCards = JsonConvert.DeserializeObject<List<NoteCard.Properties>>(File.ReadAllText("Notes.json")).Select(p => new NoteCard() { CardProperties = p }).ToList();
            }
            catch
            {
                NoteCards = new List<NoteCard>();
            }
            try
            {
                AlarmCards = JsonConvert.DeserializeObject<List<AlarmCard.Properties>>(File.ReadAllText("Alarms.json")).Select(p => new AlarmCard() { CardProperties = p }).ToList();
            }
            catch
            {
                AlarmCards = new List<AlarmCard>();
            }
            try
            {
                CheckListCards = JsonConvert.DeserializeObject<List<CheckListCard.Properties>>(File.ReadAllText("CheckLists.json")).Select(p => new CheckListCard() { CardProperties = p }).ToList();
            }
            catch
            {
                CheckListCards = new List<CheckListCard>();
            }

            ImageCards.ForEach((c) => {
                c.Show(); c.OnClosedCalled += (s, e) => 
                {
                    ImageCard card = (ImageCard)s;
                    ImageCards.Remove(card);
                    card.Close();
                };
            });
            ModelCards.ForEach((c) => {
                c.Show(); c.OnClosedCalled += (s, e) =>
                {
                    ModelCard card = (ModelCard)s;
                    ModelCards.Remove(card);
                    card.Close();
                };
            });
            NoteCards.ForEach((c) => {
                c.Show(); c.OnClosedCalled += (s, e) =>
                {
                    NoteCard card = (NoteCard)s;
                    NoteCards.Remove(card);
                    card.Close();
                };
            });
            AlarmCards.ForEach((c) => {
                c.Show(); c.OnClosedCalled += (s, e) =>
                {
                    AlarmCard card = (AlarmCard)s;
                    AlarmCards.Remove(card);
                    card.Close();
                };
            });
            CheckListCards.ForEach((c) => {
                c.Show(); c.OnClosedCalled += (s, e) =>
                {
                    CheckListCard card = (CheckListCard)s;
                    CheckListCards.Remove(card);
                    card.Close();
                };
            });

            Notifier.MouseDown += Notifier_MouseDown; ;
            Notifier.Icon = System.Drawing.SystemIcons.Application;

            Notifier.Text = "VCards";

            Notifier.Visible = true;

            try
            {
                Theme theme = JsonConvert.DeserializeObject<Theme>(File.ReadAllText("Theme.json"));

                dark = theme.NightMode;

                ThemeOpacity = theme.Opacity;

                new PaletteHelper().SetLightDark(dark);
                new PaletteHelper().ReplacePalette(theme.Pallete);

                OnOpacityChanged(ThemeOpacity);
            }
            catch { }
        }

        private void Notifier_MouseDown(object sender, WinForms.MouseEventArgs e)
        {
            if (e.Button == WinForms.MouseButtons.Right || e.Button == WinForms.MouseButtons.Left)
            {
                ContextMenu menu = (ContextMenu)FindResource("NotifierMenu");

                menu.IsOpen = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllText("Images.json", JsonConvert.SerializeObject(ImageCards.Select((c) => c.CardProperties).ToList()));
            File.WriteAllText("Models.json", JsonConvert.SerializeObject(ModelCards.Select((c) => c.CardProperties).ToList()));
            File.WriteAllText("Notes.json", JsonConvert.SerializeObject(NoteCards.Select((c) => c.CardProperties).ToList()));
            File.WriteAllText("Alarms.json", JsonConvert.SerializeObject(AlarmCards.Select((c) => c.CardProperties).ToList()));
            File.WriteAllText("CheckLists.json", JsonConvert.SerializeObject(CheckListCards.Select((c) => c.CardProperties).ToList()));

            ImageCards.ForEach((c) => c.Close());
            ModelCards.ForEach((c) => c.Close());
            NoteCards.ForEach((c) => c.Close());
            AlarmCards.ForEach((c) => c.Close());
            CheckListCards.ForEach((c) => c.Close());


            PaletteHelper p = new PaletteHelper();
            Theme theme = new Theme()
            {
                NightMode = dark,
                Opacity = ThemeOpacity,
                Pallete = p.QueryPalette()
            };
        
            File.WriteAllText("Theme.json", JsonConvert.SerializeObject(theme));
        }
        
        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            var c = new NoteCard();
            NoteCards.Add(c);
            c.Show();
            c.OnClosedCalled += (s, args) =>
            {
                NoteCard card = (NoteCard)s;
                NoteCards.Remove(card);
                card.Close();
            };
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            var c = new ImageCard();
            ImageCards.Add(c);
            c.Show();
            c.OnClosedCalled += (s, args) =>
            {
                ImageCard card = (ImageCard)s;
                ImageCards.Remove(card);
                card.Close();
            };
        }

        private void AddModel_Click(object sender, RoutedEventArgs e)
        {
            var c = new ModelCard();
            ModelCards.Add(c);
            c.Show();
            c.OnClosedCalled += (s, args) =>
            {
                ModelCard card = (ModelCard)s;
                ModelCards.Remove(card);
                card.Close();
            };
        }

        private void AddAlarm_Click(object sender, RoutedEventArgs e)
        {
            var c = new AlarmCard();
            AlarmCards.Add(c);
            c.Show();
            c.OnClosedCalled += (s, args) =>
            {
                AlarmCard card = (AlarmCard)s;
                AlarmCards.Remove(card);
                card.Close();
            };
        }

        private void AddCheckList_Click(object sender, RoutedEventArgs e)
        {
            var c = new CheckListCard();
            CheckListCards.Add(c);
            c.Show();
            c.OnClosedCalled += (s, args) =>
            {
                CheckListCard card = (CheckListCard)s;
                CheckListCards.Remove(card);
                card.Close();
            };
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        bool dark = false;
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            dark = !dark;

            new PaletteHelper().SetLightDark(dark);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = (string)((ComboBoxItem)((ComboBox)sender).SelectedItem).Content;
                item = item.Replace(" ", "");
                new PaletteHelper().ReplacePrimaryColor(item);
                new PaletteHelper().ReplaceAccentColor(item);
            }
            catch
            {

            }
        }

        public static double ThemeOpacity = 1;
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                ThemeOpacity = ((Slider)sender).Value / 100;
                OnOpacityChanged(((Slider)sender).Value / 100);
            } catch { }
        }
    }
}
