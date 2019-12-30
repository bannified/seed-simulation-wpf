using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seed_Tools;
using System.Collections.Generic;
using Suits = SimulationUnitTest.Constants.Poker.Suits;
using Cards = SimulationUnitTest.Constants.Poker.Cards;

namespace SimulationUnitTest
{
    [TestClass]
    public class PokerGameRulesTest
    {
        private GameRulesBase gameRulesInstance = new GameRulesBase(SimulationUnitTest.Constants.Poker.Decks.SampleDeck);

        // Standalone pattern tests. 

        [TestMethod]
        public void HighCard_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.EIGHT_CLUBS, Cards.THREE_DIAMONDS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.None);
        }

        [TestMethod]
        public void OnePairOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.THREE_DIAMONDS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.OnePair);
        }

        [TestMethod]
        public void TwoPairOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.TEN_SPADES };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.TwoPair);
        }

        [TestMethod]
        public void ThreeOfAKindOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.ThreeOfAKind);
        }

        [TestMethod]
        public void StraightOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.THREE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_DIAMONDS, Cards.TWO_HEARTS };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.Straight);
        }

        [TestMethod]
        public void StraightOnlyWithAcesHigh_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_DIAMONDS, Cards.TEN_SPADES, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_HEARTS };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.Straight);
        }

        [TestMethod]
        public void FlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.FIVE_CLUBS, Cards.TWO_CLUBS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.Flush);
        }

        [TestMethod]
        public void FullHouseOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.TWO_HEARTS, Cards.TWO_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.FullHouse);
        }

        [TestMethod]
        public void FourOfAKindOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.ACE_SPADES, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.FourOfAKind);
        }

        [TestMethod]
        public void StraightFlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.EIGHT_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.ACE_HEARTS };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.StraightFlush);
        }

        [TestMethod]
        public void StraightFlushOnlyAceLow_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.TWO_DIAMONDS, Cards.THREE_DIAMONDS, Cards.FOUR_DIAMONDS, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.FIVE_DIAMONDS };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.StraightFlush);
        }

        [TestMethod]
        public void RoyalFlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_CLUBS };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.RoyalFlush);
        }

        // Precedence tests, where one pattern takes precedence over another (in terms of strength)

        [TestMethod]
        public void FlushOverStraight_Test()
        {
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_SPADES };

            GameRulesBase.Pattern result = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(result == GameRulesBase.Pattern.Flush);
        }

        /// <summary>
        /// Testing that a full house pattern is chosen over a straight.
        /// Not a possible situation in a regular poker game (2 cards in hand, 5 common cards).
        /// </summary>
        [TestMethod]
        public void FullHouseOverStraight_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_CLUBS, Cards.TEN_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS, Cards.JACK_DIAMONDS };

            Assert.IsTrue(gameRulesInstance.AnalyzeHandWithCommons(testHand) == GameRulesBase.Pattern.FullHouse);
        }
    }
}
