﻿using System;
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
	/// Interaction logic for SeedToolsHome.xaml
	/// </summary>
	public partial class SeedToolsHome : Page
	{
		public SeedToolsHome()
		{
			InitializeComponent();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimulationHome simulationHomePage = new SimulationHome(this.presetListBox.SelectedItem);
            this.NavigationService.Navigate(simulationHomePage);
        }
    }
}