using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seed_Tools;

namespace SimulationUnitTest
{
    public static class Constants
    {
        public static class Poker
        {
            public static class Suits
            {
                public static readonly Suit SPADE = new Suit("Spade");
                public static readonly Suit DIAMOND = new Suit("Diamond");
                public static readonly Suit CLUB = new Suit("Club");
                public static readonly Suit HEART = new Suit("Heart");
            }

            public static class Cards
            {
                public static readonly CompleteCardData TWO_SPADES = new CompleteCardData(Suits.SPADE, 2);
                public static readonly CompleteCardData THREE_SPADES = new CompleteCardData(Suits.SPADE, 3);
                public static readonly CompleteCardData FOUR_SPADES = new CompleteCardData(Suits.SPADE, 4);
                public static readonly CompleteCardData FIVE_SPADES = new CompleteCardData(Suits.SPADE, 5);
                public static readonly CompleteCardData SIX_SPADES = new CompleteCardData(Suits.SPADE, 6);
                public static readonly CompleteCardData SEVEN_SPADES = new CompleteCardData(Suits.SPADE, 7);
                public static readonly CompleteCardData EIGHT_SPADES = new CompleteCardData(Suits.SPADE, 8);
                public static readonly CompleteCardData NINE_SPADES = new CompleteCardData(Suits.SPADE, 9);
                public static readonly CompleteCardData TEN_SPADES = new CompleteCardData(Suits.SPADE, 10);
                public static readonly CompleteCardData JACK_SPADES = new CompleteCardData(Suits.SPADE, 11);
                public static readonly CompleteCardData QUEEN_SPADES = new CompleteCardData(Suits.SPADE, 12);
                public static readonly CompleteCardData KING_SPADES = new CompleteCardData(Suits.SPADE, 13);
                public static readonly CompleteCardData ACE_SPADES = new CompleteCardData(Suits.SPADE, 14);

                public static readonly CompleteCardData TWO_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 2);
                public static readonly CompleteCardData THREE_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 3);
                public static readonly CompleteCardData FOUR_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 4);
                public static readonly CompleteCardData FIVE_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 5);
                public static readonly CompleteCardData SIX_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 6);
                public static readonly CompleteCardData SEVEN_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 7);
                public static readonly CompleteCardData EIGHT_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 8);
                public static readonly CompleteCardData NINE_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 9);
                public static readonly CompleteCardData TEN_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 10);
                public static readonly CompleteCardData JACK_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 11);
                public static readonly CompleteCardData QUEEN_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 12);
                public static readonly CompleteCardData KING_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 13);
                public static readonly CompleteCardData ACE_DIAMONDS = new CompleteCardData(Suits.DIAMOND, 14);

                public static readonly CompleteCardData TWO_CLUBS = new CompleteCardData(Suits.CLUB, 2);
                public static readonly CompleteCardData THREE_CLUBS = new CompleteCardData(Suits.CLUB, 3);
                public static readonly CompleteCardData FOUR_CLUBS = new CompleteCardData(Suits.CLUB, 4);
                public static readonly CompleteCardData FIVE_CLUBS = new CompleteCardData(Suits.CLUB, 5);
                public static readonly CompleteCardData SIX_CLUBS = new CompleteCardData(Suits.CLUB, 6);
                public static readonly CompleteCardData SEVEN_CLUBS = new CompleteCardData(Suits.CLUB, 7);
                public static readonly CompleteCardData EIGHT_CLUBS = new CompleteCardData(Suits.CLUB, 8);
                public static readonly CompleteCardData NINE_CLUBS = new CompleteCardData(Suits.CLUB, 9);
                public static readonly CompleteCardData TEN_CLUBS = new CompleteCardData(Suits.CLUB, 10);
                public static readonly CompleteCardData JACK_CLUBS = new CompleteCardData(Suits.CLUB, 11);
                public static readonly CompleteCardData QUEEN_CLUBS = new CompleteCardData(Suits.CLUB, 12);
                public static readonly CompleteCardData KING_CLUBS = new CompleteCardData(Suits.CLUB, 13);
                public static readonly CompleteCardData ACE_CLUBS = new CompleteCardData(Suits.CLUB, 14);

                public static readonly CompleteCardData TWO_HEARTS = new CompleteCardData(Suits.HEART, 2);
                public static readonly CompleteCardData THREE_HEARTS = new CompleteCardData(Suits.HEART, 3);
                public static readonly CompleteCardData FOUR_HEARTS = new CompleteCardData(Suits.HEART, 4);
                public static readonly CompleteCardData FIVE_HEARTS = new CompleteCardData(Suits.HEART, 5);
                public static readonly CompleteCardData SIX_HEARTS = new CompleteCardData(Suits.HEART, 6);
                public static readonly CompleteCardData SEVEN_HEARTS = new CompleteCardData(Suits.HEART, 7);
                public static readonly CompleteCardData EIGHT_HEARTS = new CompleteCardData(Suits.HEART, 8);
                public static readonly CompleteCardData NINE_HEARTS = new CompleteCardData(Suits.HEART, 9);
                public static readonly CompleteCardData TEN_HEARTS = new CompleteCardData(Suits.HEART, 10);
                public static readonly CompleteCardData JACK_HEARTS = new CompleteCardData(Suits.HEART, 11);
                public static readonly CompleteCardData QUEEN_HEARTS = new CompleteCardData(Suits.HEART, 12);
                public static readonly CompleteCardData KING_HEARTS = new CompleteCardData(Suits.HEART, 13);
                public static readonly CompleteCardData ACE_HEARTS = new CompleteCardData(Suits.HEART, 14);
            }

            public static class Decks
            {
                public static readonly CompleteDeckData SampleDeck =
                    new CompleteDeckData(
                        new List<CompleteCardData>()
                        {
                            Cards.TWO_SPADES,
                            Cards.THREE_SPADES,
                            Cards.FOUR_SPADES,
                            Cards.FIVE_SPADES,
                            Cards.SIX_SPADES,
                            Cards.SEVEN_SPADES,
                            Cards.EIGHT_SPADES,
                            Cards.NINE_SPADES,
                            Cards.TEN_SPADES,
                            Cards.JACK_SPADES,
                            Cards.QUEEN_SPADES,
                            Cards.KING_SPADES,
                            Cards.ACE_SPADES,

                            Cards.TWO_DIAMONDS,
                            Cards.THREE_DIAMONDS,
                            Cards.FOUR_DIAMONDS,
                            Cards.FIVE_DIAMONDS,
                            Cards.SIX_DIAMONDS,
                            Cards.SEVEN_DIAMONDS,
                            Cards.EIGHT_DIAMONDS,
                            Cards.NINE_DIAMONDS,
                            Cards.TEN_DIAMONDS,
                            Cards.JACK_DIAMONDS,
                            Cards.QUEEN_DIAMONDS,
                            Cards.KING_DIAMONDS,
                            Cards.ACE_DIAMONDS,

                            Cards.TWO_CLUBS,
                            Cards.THREE_CLUBS,
                            Cards.FOUR_CLUBS,
                            Cards.FIVE_CLUBS,
                            Cards.SIX_CLUBS,
                            Cards.SEVEN_CLUBS,
                            Cards.EIGHT_CLUBS,
                            Cards.NINE_CLUBS,
                            Cards.TEN_CLUBS,
                            Cards.JACK_CLUBS,
                            Cards.QUEEN_CLUBS,
                            Cards.KING_CLUBS,
                            Cards.ACE_CLUBS,

                            Cards.TWO_HEARTS,
                            Cards.THREE_HEARTS,
                            Cards.FOUR_HEARTS,
                            Cards.FIVE_HEARTS,
                            Cards.SIX_HEARTS,
                            Cards.SEVEN_HEARTS,
                            Cards.EIGHT_HEARTS,
                            Cards.NINE_HEARTS,
                            Cards.TEN_HEARTS,
                            Cards.JACK_HEARTS,
                            Cards.QUEEN_HEARTS,
                            Cards.KING_HEARTS,
                            Cards.ACE_HEARTS,
                        }
                    );
            }
        }
    }
}
