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
using Json.Net;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for DeckEditPage.xaml
    /// </summary>
    public partial class DeckEditPage : Page
    {
        public DeckData ActiveDeckData { get; set; }

        public System.Action<DeckData> OnDeckLoaded;

        public DeckEditPage()
        {
            InitializeComponent();
            ActiveDeckData = new DeckData();
            OnDeckLoaded += RefreshDeckDisplay;
        }

        private void OnRemoveCardClicked(object sender, float e)
        {

        }

        private void OnAddCardClicked(object sender, float e)
        {

        }

        private void DeckLoadClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = App.GetRootPath();
            dialog.DefaultExt = ".dek";
            dialog.Multiselect = false;
            dialog.Filter = "Seed Deck Files (*.dek)|*.dek";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                if (System.IO.File.Exists(filename))
                {
                    string loadedText = System.IO.File.ReadAllText(filename);
                    DeckData deck = JsonNet.Deserialize<DeckData>(loadedText);
                    ActiveDeckData = deck;
                    OnDeckLoaded?.Invoke(deck);
                }
            }
        }

        private void DeckSaveAsClicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.InitialDirectory = App.GetRootPath();
            dialog.DefaultExt = ".dek";
            dialog.Filter = "Seed Deck Files (*.dek)|*.dek";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                System.IO.FileStream fs = System.IO.File.Create(filename);
                fs.Close();
                string resultJson = JsonNet.Serialize(ActiveDeckData);
                System.IO.File.WriteAllText(filename, resultJson);
            }
        }

        private void DeckNameLostFocus(object sender, RoutedEventArgs e)
        {
            ActiveDeckData.DisplayName = DeckNameTextBox.Text;
        }

        private void RefreshDeckDisplay(DeckData data)
        {
            DeckNameTextBox.Text = data.DisplayName;
        }
    }
}
