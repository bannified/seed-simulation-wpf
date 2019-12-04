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
        public CardData cardData
        {
            get { return (CardData)GetValue(CardDataProperty); }
            set { SetValue(CardDataProperty, value); }
        }

        public static readonly DependencyProperty CardDataProperty =
            DependencyProperty.Register("cardData", typeof(CardData), typeof(Card), new PropertyMetadata(new CardData(), OnDataUpdated));

        public static readonly DependencyProperty MainImageSourceProperty =
            DependencyProperty.Register(nameof(MainImageSource), typeof(ImageSource), typeof(Card),
                new PropertyMetadata(null));

        public ImageSource MainImageSource
        {
            get { return (ImageSource)GetValue(MainImageSourceProperty); }
            set { SetValue(MainImageSourceProperty, value); }
        }

        public Card()
        {
            InitializeComponent();
            cardData = new CardData();
        }

        public Card(CardData data)
        {
            InitializeComponent();
            cardData = data;
        }

        private static void OnDataUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Card card = d as Card;
            if (card != null && card.cardData != null)
            {
                if (System.IO.File.Exists(card.cardData.MainImageSourcePath))
                {
                    card.MainImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath(card.cardData.MainImageSourcePath), UriKind.RelativeOrAbsolute));
                } else
                {
                    card.MainImageSource = null;
                }
            }
        }
    }
}
