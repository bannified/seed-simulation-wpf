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

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Simulation_Click(object sender, RoutedEventArgs e)
        {
            SimulationPage simulationPage = new SimulationPage();
            this.NavigationService.Navigate(simulationPage);
        }

        private void DeckButton_Click(object sender, RoutedEventArgs e)
        {
            DeckEditPage deckEditPage = new DeckEditPage();
            this.NavigationService.Navigate(deckEditPage);
        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            RulesEditPage ruleEditPage = new RulesEditPage();
            this.NavigationService.Navigate(ruleEditPage);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
