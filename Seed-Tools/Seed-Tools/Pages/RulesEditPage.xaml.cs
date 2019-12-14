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
using System.Collections.ObjectModel;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for RulesEditPage.xaml
    /// </summary>
    public partial class RulesEditPage : Page
    {
        public RulesEditPage()
        {
            InitializeComponent();
            this.DataContext = this;

            SuitsListBox.ItemsSource = AllSuits;
        }

        public Suit CurrentSuit
        {
            get { return (Suit)GetValue(CurrentSuitProperty); }
            set { SetValue(CurrentSuitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentSuit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentSuitProperty =
            DependencyProperty.Register("CurrentSuit", typeof(Suit), typeof(RulesEditPage), new PropertyMetadata(new Suit()));

        public ImageSource SuitImageSource
        {
            get { return (ImageSource)GetValue(SuitImageSourceProperty); }
            set { SetValue(SuitImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SuitImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuitImageSourceProperty =
            DependencyProperty.Register("SuitImageSource", typeof(ImageSource), typeof(RulesEditPage), new PropertyMetadata(null));

        public ObservableCollection<Suit> AllSuits { get; set; } = new ObservableCollection<Suit>();

        private void SuitSelected(object sender, SelectionChangedEventArgs e)
        {
            CurrentSuit = (Suit)SuitsListBox.SelectedItem;
        }

        private void SuitNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            CurrentSuit.Name = SuitNameTextBox.Text;
        }

        private void SuitValueTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            int res = 0;
            int.TryParse(SuitValueTextBox.Text, out res);
            CurrentSuit.Value = res;
            SuitValueTextBox.Text = res.ToString();
        }

        private void SelectSuitImageButtonClicked(object sender, RoutedEventArgs e)
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

            if (result == true)
            {
                string filepath = dialog.FileName;
                if (System.IO.File.Exists(filepath)) // exists, set the image path
                {
                    if (CurrentSuit != null)
                    {
                        CurrentSuit.ImagePath = filepath;
                    }
                    else
                    {
                        MessageBox.Show("Select a valid card first", "Card not selected");
                    }
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
            }
        }

        private void AddNewSuitClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
