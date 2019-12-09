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
using System.Windows.Shapes;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for TextInputWindow.xaml
    /// Simple dialog to input a string of letters.
    /// </summary>
    public partial class TextInputWindow : Window
    {
        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(TextInputWindow), new PropertyMetadata("TextInputWindow"));

        public string InputBoxTitle
        {
            get { return (string)GetValue(BoxTitleProperty); }
            set { SetValue(BoxTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BoxTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoxTitleProperty =
            DependencyProperty.Register("InputBoxTitle", typeof(string), typeof(TextInputWindow), new PropertyMetadata("DefaultTitle"));

        public TextInputWindow()
        {
            InitializeComponent();
        }

        public TextInputWindow(string windowTitle, string inputBoxTitle) : this()
        {
            this.WindowTitle = windowTitle;
            this.InputBoxTitle = inputBoxTitle;
        }

        /// <summary>
        /// Cancel Button clicked, returns false for result.
        /// </summary>
        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// Cancel Button clicked, returns true for result.
        /// </summary>
        private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
