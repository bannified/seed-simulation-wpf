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
        private PokerGameRules gameRulesInstance = new PokerGameRules(SimulationUnitTest.Constants.Poker.Decks.SampleDeck);

        private const int minStrength = 2;

        // Standalone pattern tests. 

        [TestMethod]
        public void HighCard_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.EIGHT_CLUBS, Cards.THREE_DIAMONDS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);
            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.None);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void OnePairOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.THREE_DIAMONDS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.OnePair);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void TwoPairOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.TEN_SPADES };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.TwoPair);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void ThreeOfAKindOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.ThreeOfAKind);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void StraightOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.THREE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_DIAMONDS, Cards.TWO_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.Straight);
            Assert.IsTrue(res.strengthValue == Cards.KING_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void StraightOnlyWithAcesHigh_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_DIAMONDS, Cards.TEN_SPADES, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.Straight);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void FlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.FIVE_CLUBS, Cards.TWO_CLUBS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.Flush);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void FullHouseOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.KING_CLUBS, Cards.TWO_HEARTS, Cards.TWO_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.FullHouse);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void FourOfAKindOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.ACE_SPADES, Cards.QUEEN_HEARTS, Cards.TEN_SPADES, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.ACE_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.FourOfAKind);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void StraightFlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.EIGHT_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.ACE_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.StraightFlush);
            Assert.IsTrue(res.strengthValue == Cards.QUEEN_CLUBS.StrengthValue);
        }

        [TestMethod]
        public void StraightFlushOnlyAceLow_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_DIAMONDS, Cards.TWO_DIAMONDS, Cards.THREE_DIAMONDS, Cards.FOUR_DIAMONDS, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.FIVE_DIAMONDS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.StraightFlush);
            Assert.IsTrue(res.strengthValue == Cards.FIVE_DIAMONDS.StrengthValue);
        }
        [TestMethod]
        public void StraightFlushHighOverStraightHigh_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.TWO_DIAMONDS, Cards.EIGHT_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_CLUBS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_HEARTS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.StraightFlush);
            Assert.IsTrue(res.strengthValue == Cards.QUEEN_CLUBS.StrengthValue);
        }


        [TestMethod]
        public void RoyalFlushOnly_Test()
        {
            // Setup
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_CLUBS };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.RoyalFlush);
        }

        // Precedence tests, where one pattern takes precedence over another (in terms of strength)

        [TestMethod]
        public void FlushOverStraight_Test()
        {
            List<CompleteCardData> testCommons = new List<CompleteCardData>() { Cards.ACE_CLUBS, Cards.FIVE_CLUBS, Cards.QUEEN_CLUBS, Cards.TEN_CLUBS, Cards.NINE_DIAMONDS };
            gameRulesInstance.AnalyzeCommons(testCommons);

            List<CompleteCardData> testHand = new List<CompleteCardData>() { Cards.JACK_CLUBS, Cards.KING_SPADES };

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.Flush);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
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

            PokerGameRules.Result res = gameRulesInstance.AnalyzeHandWithCommons(testHand);

            Assert.IsTrue(res.pattern == PokerGameRules.Pattern.FullHouse);
            Assert.IsTrue(res.strengthValue == Cards.ACE_CLUBS.StrengthValue);
        }
    }
}
