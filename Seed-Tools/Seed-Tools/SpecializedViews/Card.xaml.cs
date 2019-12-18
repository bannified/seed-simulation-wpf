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
    /// A view for a typical card in a deck.
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

        public ImageSource SuitImageSource
        {
            get { return (ImageSource)GetValue(SuitImageSourceProperty); }
            set { SetValue(SuitImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SuitImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SuitImageSourceProperty =
            DependencyProperty.Register("SuitImageSource", typeof(ImageSource), typeof(Card), new PropertyMetadata(null));

        public Card()
        {
            // to prevent error in design mode.
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            InitializeComponent();
            cardData = new CardData();
            this.DataContext = this;
        }

        public Card(CardData data) : this()
        {
            cardData = data;
        }

        private static void OnDataUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Card cardView = d as Card;
            if (cardView != null && cardView.cardData != null)
            {
                if (System.IO.File.Exists(cardView.cardData.MainImageSourcePath))
                {
                    cardView.MainImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath(cardView.cardData.MainImageSourcePath), UriKind.RelativeOrAbsolute));
                } else
                {
                    cardView.MainImageSource = null;
                }

                Suit resSuit = null;

                App.CastedInstance.SuitsCollection.TryGetValue(cardView.cardData.Suit1, out resSuit);

                if (resSuit == null) return; // Suit does not exist

                if (System.IO.File.Exists(resSuit.ImagePath))
                {
                    cardView.SuitImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath(resSuit.ImagePath), UriKind.RelativeOrAbsolute));
                } else
                {
                    cardView.SuitImageSource = null;
                }
            }
        }
    }
}
