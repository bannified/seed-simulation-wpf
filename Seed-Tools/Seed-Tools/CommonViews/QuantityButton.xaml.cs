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
    /// Interaction logic for QuantityButton.xaml
    /// Simple Button that fires an event with a user-defined numeric value when clicked.
    /// </summary>
    public partial class QuantityButton : UserControl
    {
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(nameof(ButtonText), typeof(string), typeof(QuantityButton),
                new FrameworkPropertyMetadata(" ", FrameworkPropertyMetadataOptions.AffectsRender));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public float CurrentQuantity
        {
            get { return (float)GetValue(QuantityProperty); }
            set { SetValue(QuantityProperty, value); }
        }

        public event EventHandler<float> OnMainButtonClicked;

        private void MainButtonClicked(object sender, EventArgs args)
        {
            OnMainButtonClicked?.Invoke(sender, CurrentQuantity);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register("CurrentQuantity", typeof(float), typeof(QuantityButton), new PropertyMetadata(0.0f, OnInputQuantityBoxValueUpdate));

        private static void OnInputQuantityBoxValueUpdate(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            QuantityButton button = d as QuantityButton;
            if (d != null)
            {
                button.InputQuantityBox.Text = button.CurrentQuantity.ToString();
            }
            Console.WriteLine("CurrentQuantity: " + button.CurrentQuantity);
        }

        public QuantityButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks input to ensure that it is a number.
        /// </summary>
        private void QuantityInputCheck(object sender, TextCompositionEventArgs e)
        {
            int res = 0;
            e.Handled = !(int.TryParse(e.Text, out res) || (e.Text == "."));
        }

        /// <summary>
        /// Checks that text is a number
        /// </summary>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            float res = 0;
            float.TryParse(InputQuantityBox.Text, out res);
            CurrentQuantity = res;
        }
    }
}
