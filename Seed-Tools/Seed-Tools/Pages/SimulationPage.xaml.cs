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

        public int NumOfSimulationRuns
        {
            get { return (int)GetValue(NumOfSimulationRunsProperty); }
            set { SetValue(NumOfSimulationRunsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumOfSimulationRuns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumOfSimulationRunsProperty =
            DependencyProperty.Register("NumOfSimulationRuns", typeof(int), typeof(SimulationPage), new PropertyMetadata(1));

        public int NumOfPlayers
        {
            get { return (int)GetValue(NumOfPlayersProperty); }
            set { SetValue(NumOfPlayersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumOfPlayers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumOfPlayersProperty =
            DependencyProperty.Register("NumOfPlayers", typeof(int), typeof(SimulationPage), new PropertyMetadata(Seed_Tools.Properties.Settings.Default.NumberOfPlayers));

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

            NumOfPlayers = Seed_Tools.Properties.Settings.Default.NumberOfPlayers;
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

        private void StartSimulation(object sender, RoutedEventArgs e)
        {
            Simulation sim = new Simulation(ActiveDeckData, 2, 5, NumOfSimulationRuns, NumOfPlayers);

        }

        private void IntegerInputCheck(object sender, TextCompositionEventArgs e)
        {
            int res = 0;
            e.Handled = !(int.TryParse(e.Text, out res));
        }

        private void OnNumberOfRunsTextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (sender == null) return;

            int res = 0;
            int.TryParse(textBox.Text, out res);
            NumOfSimulationRuns = Math.Max(1, res);
        }

        private void OnNumberOfPlayersTextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (sender == null) return;

            int res = 0;
            int.TryParse(textBox.Text, out res);
            NumOfPlayers = Math.Max(App.Constants.SimulationParameters.MINIMUM_NUM_OF_PLAYERS, res);
            Seed_Tools.Properties.Settings.Default.NumberOfPlayers = NumOfPlayers;
        }
    }
}
