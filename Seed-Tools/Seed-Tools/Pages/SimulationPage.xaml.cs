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
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for SimulationPage.xaml
    /// </summary>
    public partial class SimulationPage : Page
    {
        // Current Active Deck
        public DeckData ActiveDeckData { get; set; }

        public ObservableCollection<DeckEntryData> DeckCardViews { get; set; }

        public System.Action OnDeckLoaded;

        public SimulationPage()
        {
            InitializeComponent();
            this.DataContext = this;

            // Setup Deck's view
            DeckCardViews = new ObservableCollection<DeckEntryData>();
            DeckCardsListBox.ItemsSource = DeckCardViews;

            OnDeckLoaded += RefreshDeckList;

            if (System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.ActiveDeckPath))
            {
                LoadDeckAtPath(Seed_Tools.Properties.Settings.Default.ActiveDeckPath);
            }
        }

        /// <summary>
        /// Loads a deck file given its relative or full path
        /// </summary>
        /// <param name="path">Relative or full path of deck file</param>
        private void LoadDeckAtPath(string path)
        {
            string loadedText = System.IO.File.ReadAllText(path);
            DeckData deck = JsonConvert.DeserializeObject<DeckData>(loadedText);
            if (deck == null)
            {
                Console.WriteLine("Null Deck");
            }
            ActiveDeckData = deck;
            OnDeckLoaded?.Invoke();
        }

        private void RefreshDeckList()
        {
            DeckCardViews.Clear();

            foreach (var kv in ActiveDeckData.CardIdToCount)
            {
                CardData card = null;
                if (App.CastedInstance.CardLibrary.TryGetValue(kv.Key, out card)) {
                    DeckEntryData entry = new DeckEntryData(card, kv.Value);
                    DeckCardViews.Add(entry);
                }
            }
        }

        private void DeckLoadClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = App.GetRootPath();
            dialog.DefaultExt = App.Constants.FileExtensions.DECK;
            dialog.Multiselect = false;
            dialog.Filter = string.Format("Seed Deck Files (*{0})|*{0}", App.Constants.FileExtensions.DECK);

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                if (System.IO.File.Exists(filename))
                {
                    LoadDeckAtPath(filename);
                    // Set active deck path
                    string relative = App.GetRelativePathFromFullPath(filename);
                    Seed_Tools.Properties.Settings.Default.ActiveDeckPath = relative;
                }
            }
        }
    }
}
