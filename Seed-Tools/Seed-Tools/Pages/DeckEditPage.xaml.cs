using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for DeckEditPage.xaml
    /// </summary>
    public partial class DeckEditPage : Page
    {
        // Current Card that's shown on the Deck Edit Page
        public CardData CurrentCard
        {
            get { return (CardData)GetValue(CurrentCardProperty); }
            set { SetValue(CurrentCardProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentCard.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCardProperty =
            DependencyProperty.Register("CurrentCard", typeof(CardData), typeof(DeckEditPage), new PropertyMetadata(new CardData()));

        /**
         * For All Cards Views
         */
        // Card Views for AllCardsViewsList
        public ObservableCollection<CardData> CardViews { get; set; }

        public int CurrentSelectedCardIndex;

        /**
         * For Deck list cards
         */
        // Current Active Deck
        public DeckData ActiveDeckData { get; set; }

        public ObservableCollection<DeckEntryData> DeckCardViews { get; set; }

        /*
         * For Suits
         */
         public ObservableCollection<string> Suits { get; set; }

        /**
         * Events
         */
        public System.Action OnDeckLoaded;
        public System.Action OnCardLibraryUpdated;

        public DeckEditPage()
        {
            InitializeComponent();
            this.DataContext = this;

            ActiveDeckData = new DeckData();

            // Setup Library's Cards List
            CardViews = new ObservableCollection<CardData>();
            AllCardsListBox.ItemsSource = CardViews;
            RefreshCardLibrary();
            OnCardLibraryUpdated += RefreshDeckDisplay;

            if (CardViews.Count > 0)
            {
                AllCardsListBox.SelectedIndex = CurrentSelectedCardIndex;
            }

            // Setup Deck's view
            DeckCardViews = new ObservableCollection<DeckEntryData>();
            DeckCardsListBox.ItemsSource = DeckCardViews;
            OnDeckLoaded += RefreshDeckDisplay;
            OnCardLibraryUpdated += RefreshCardLibrary;

            if (System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.ActiveDeckPath))
            {
                LoadDeckAtPath(Seed_Tools.Properties.Settings.Default.ActiveDeckPath);
            }

            // Setup Main Card view
            SizeChanged += ResizeCardView;

            // Load all the suits
            Suits = new ObservableCollection<string>();

            SuitComboBox.ItemsSource = Suits;
            foreach (var kv in App.CastedInstance.SuitsCollection)
            {
                Suits.Add(kv.Key);
            }
        }

        /// <summary>
        /// Resizes the Card's view to always respect our given aspect ratio of a card given in App.Constant
        /// </summary>
        private void ResizeCardView(object sender, SizeChangedEventArgs e)
        {
            DeckEditPage page = sender as DeckEditPage;
            if (page != null)
            {
                double resultHeight = App.Constants.Scaling.CardHeightToWidthRatio * page.CardSizingGrid.ActualWidth;
                double resultWidth = App.Constants.Scaling.CardWidthToHeightRatio * page.CardSizingGrid.ActualHeight;

                if (resultHeight > page.CardSizingGrid.ActualHeight) // need to shrink height, take width to be fixed
                {
                    page.card.Width = resultWidth;
                    page.card.Height = page.CardSizingGrid.ActualHeight;
                }
                else
                {
                    page.card.Height = resultHeight;
                    page.card.Width = page.CardSizingGrid.ActualWidth;
                }

            }
        }

        /// <summary>
        /// RemoveCardButton Clicked
        /// Removes x number of CurrentCard from the ActiveDeck
        /// </summary>
        private void OnRemoveCardClicked(object sender, float e)
        {
            if (RemoveCardButton.CurrentQuantity <= 0)
            {
                return;
            }
            if (ActiveDeckData.CardIdToCount.ContainsKey(CurrentCard.id))
            {
                ActiveDeckData.CardIdToCount[CurrentCard.id] -= (int)Math.Round(RemoveCardButton.CurrentQuantity);
                if (ActiveDeckData.CardIdToCount[CurrentCard.id] <= 0)
                {
                    ActiveDeckData.CardIdToCount.Remove(CurrentCard.id);
                }
            }

            RefreshDeckDisplay();
        }

        /// <summary>
        /// AddCardButton Clicked
        /// Adds x number of CurrentCard to the ActiveDeck
        /// </summary>
        private void OnAddCardClicked(object sender, float e)
        {
            if (AddCardButton.CurrentQuantity <= 0)
            {
                return;
            }
            if (ActiveDeckData.CardIdToCount.ContainsKey(CurrentCard.id))
            {
                ActiveDeckData.CardIdToCount[CurrentCard.id] += (int)Math.Round(AddCardButton.CurrentQuantity);
            } else
            {
                ActiveDeckData.CardIdToCount.Add(CurrentCard.id, (int)Math.Round(AddCardButton.CurrentQuantity));
            }

            RefreshDeckDisplay();
        }

        /// <summary>
        /// Brings up the a Windows Dialog to select a deck file to load
        /// </summary>
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

        /// <summary>
        /// Brings up the a Windows Dialog to save the current Active Deck to a selected path
        /// </summary>
        private void DeckSaveAsClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = App.GetRootPath();
            dialog.DefaultExt = App.Constants.FileExtensions.DECK;
            dialog.Filter = string.Format("Seed Deck Files (*{0})|*{0}", App.Constants.FileExtensions.DECK);

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;

                // Set active deck path
                string relative = App.GetRelativePathFromFullPath(filename);
                Seed_Tools.Properties.Settings.Default.ActiveDeckPath = relative;

                System.IO.FileStream fs = System.IO.File.Create(filename);
                fs.Close();
                string resultJson = JsonConvert.SerializeObject(ActiveDeckData, Formatting.Indented);
                System.IO.File.WriteAllText(filename, resultJson);
            }
        }

        /// <summary>
        /// Called when DeckNameTextBox loses focus. Saves the ActiveDeck's display name
        /// </summary>
        private void DeckNameLostFocus(object sender, RoutedEventArgs e)
        {
            ActiveDeckData.DisplayName = DeckNameTextBox.Text;
        }

        /// <summary>
        /// Refreshes the whole deck's display based on input data.
        /// This includes the deck's name and the list of cards.
        /// </summary>
        /// <param name="data">Deck Data</param>
        private void RefreshDeckDisplay()
        {
            DeckNameTextBox.Text = ActiveDeckData.DisplayName;

            DeckCardViews.Clear();

            foreach (var kv in ActiveDeckData.CardIdToCount)
            {
                CardData card;
                App.CastedInstance.CardLibrary.TryGetValue(kv.Key, out card);
                if (card != null)
                {
                    DeckEntryData entry = new DeckEntryData(card, kv.Value);
                    DeckCardViews.Add(entry);
                }
            }
        }

        /// <summary>
        /// Brings up Dialog to import an image.
        /// Copies the image into the app's installation folder.
        /// </summary>
        private void ImportImageButtonClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = App.GetRootPath();
            dialog.Multiselect = false;
            dialog.Filter = App.Constants.FileDialogFilters.GRAPHICS_FILTER;

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filepath = dialog.FileName;
                string destinationPath = App.Constants.Paths.CARD_IMAGES_PATH + System.IO.Path.GetFileName(filepath);
                if (!System.IO.File.Exists(destinationPath)) // does not exist, go ahead and copy
                {
                    if (!System.IO.Directory.Exists(App.Constants.Paths.CARD_IMAGES_PATH))
                    {
                        System.IO.Directory.CreateDirectory(App.Constants.Paths.CARD_IMAGES_PATH);
                    }
                    System.IO.File.Copy(filepath, destinationPath);
                } else
                {
                    MessageBox.Show("File already exists. Try renaming the file you're important or the file that currently exists in the 'images' directory",
                        "Image File already exists");
                }
            }
        }

        /// <summary>
        /// Brings up a dialog to select an image to set for the CurrentCard.
        /// </summary>
        private void SetImageButtonClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            if (!System.IO.Directory.Exists(App.Constants.Paths.CARD_IMAGES_PATH))
            {
                System.IO.Directory.CreateDirectory(App.Constants.Paths.CARD_IMAGES_PATH);
            }
            dialog.InitialDirectory = System.IO.Path.GetFullPath(App.Constants.Paths.CARD_IMAGES_PATH);
            dialog.Multiselect = false;
            dialog.Filter = App.Constants.FileDialogFilters.GRAPHICS_FILTER;

            Nullable<bool> result = dialog.ShowDialog();

            CardData savedCard = CurrentCard;

            if (result == true)
            {
                string filepath = dialog.FileName;
                if (System.IO.File.Exists(filepath)) // exists, set the image path
                {
                    if (CurrentCard != null)
                    {
                        CurrentCard.MainImageSourcePath = App.GetRelativePathFromFullPath(filepath);
                    } else
                    {
                        MessageBox.Show("Select a valid card first", "Card not selected");
                    }
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
            }

            // Workaround to force the binding to update.
            CurrentCard = null;
            CurrentCard = savedCard;
        }

        /// <summary>
        /// Adds a new card to the Library.
        /// </summary>
        private void AddNewCardClicked(object sender, RoutedEventArgs e)
        {
            string randomId = App.CastedInstance.random.Next().ToString();
            while (App.CastedInstance.CardLibrary.ContainsKey(randomId))
            {
                randomId = App.CastedInstance.random.Next().ToString(); // regenerate id
            }
            App.CastedInstance.CardLibrary.Add(randomId, new CardData());

            CurrentCard = App.CastedInstance.CardLibrary[randomId];
            OnCardLibraryUpdated?.Invoke();
        }

        /// <summary>
        /// Refreshes the list of cards in Library.
        /// </summary>
        public void RefreshCardLibrary()
        {
            CardViews.Clear();

            foreach (var kv in App.CastedInstance.CardLibrary)
            {
                CardViews.Add(kv.Value);
            }
        }

        /// <summary>
        /// Sets CurrentCard based on selection from AllCardsListBox
        /// </summary>
        private void CardSelectedFromList(object sender, SelectionChangedEventArgs e)
        {
            if (AllCardsListBox.SelectedItem == null)
            {
                return;
            }

            CurrentCard = AllCardsListBox.SelectedItem as CardData;
        }

        /// <summary>
        /// Brings up a dialog to set the CurrentCard's name.
        /// </summary>
        private void SetNameButtonClicked(object sender, RoutedEventArgs e)
        {
            TextInputWindow inputWindow = new TextInputWindow("Change Card Title", "New Card Title");

            Nullable<bool> result = inputWindow.ShowDialog();

            CardData savedCard = CurrentCard;

            if (result == true)
            {
                CurrentCard.Name = inputWindow.ResultTextBox.Text;
            }

            RefreshCardLibrary();

            // Workaround to force the binding to update.
            CurrentCard = null;
            CurrentCard = savedCard;
        }

        private void CardSelectedFromDeckList(object sender, SelectionChangedEventArgs e)
        {
            if (DeckCardsListBox.SelectedItem == null)
            {
                return;
            }

            DeckEntryData entry = DeckCardsListBox.SelectedItem as DeckEntryData;
            CurrentCard = entry.Card;

            DeckCardsListBox.SelectedItem = null;

            AllCardsListBox.SelectedItem = CurrentCard;
        }

        private void SuitComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = SuitComboBox.SelectedValue as string;
            if (selected != null && App.CastedInstance.SuitsCollection.ContainsKey(selected))
            {
                CurrentCard.Suit1 = selected;
            } else
            {
                CurrentCard.Suit1 = "";
                SuitComboBox.SelectedValue = "";
            }

            Suit resSuit = null;

            App.CastedInstance.SuitsCollection.TryGetValue(CurrentCard.Suit1, out resSuit);

            if (resSuit == null) return; // Suit does not exist

            if (System.IO.File.Exists(resSuit.ImagePath))
            {
                card.SuitImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath(resSuit.ImagePath), UriKind.RelativeOrAbsolute));
            }
            else
            {
                card.SuitImageSource = null;
            }
        }

        private void StrengthValueTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CurrentCard == null)
            {
                return;
            }

            int res = 0;
            if (int.TryParse(StrengthValueTextBox.Text, out res))
            {
                CurrentCard.StrengthValue = res;
            } else
            {
                CurrentCard.StrengthValue = 0;
                StrengthValueTextBox.Text = 0.ToString();
            }
        }
    }
}
