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
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : UserControl
    {
        public static readonly DependencyProperty MainImageSourceProperty =
            DependencyProperty.Register(nameof(MainImageSource), typeof(string), typeof(Card),
                new PropertyMetadata("images/sample-card.png", new PropertyChangedCallback(OnMainImageSourceChanged)));

        public string MainImageSource
        {
            get { return (string)GetValue(MainImageSourceProperty); }
            set { SetValue(MainImageSourceProperty, value); }
        }

        public Card()
        {
            InitializeComponent();
        }

        private static void OnMainImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Card card = d as Card;
            if (card != null)
            {
                card.MainImage.Source = new BitmapImage(new Uri(card.MainImageSource, UriKind.RelativeOrAbsolute));
            }
            Console.WriteLine("Card changed to MainImageSource: " + card.MainImageSource);
        }
    }
}
