using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class GameRulesBase
    {
        public enum Pattern
        {
            None = 0, // high
            OnePair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            FourOfAKind = 7,
            StraightFlush = 8,
            RoyalFlush = 9
        }

        public int MaxNumCards = 5; // maximum number of cards taken into consideration

        // metadata
        private int m_MinStrength;
        private int m_MaxStrength;

        private int m_NumSuits;

        private List<int> CommonsStrengthCount; // the number of cards of said strength
        private Dictionary<Suit, int> CommonsSuitToCountDict;

        private List<CompleteCardData> m_CommonPool;

        public GameRulesBase()
        {

        }

        public GameRulesBase(CompleteDeckData deck)
        {
            ProcessDeck(deck);
        }

        /// <summary>
        /// Processes the deck for metadata, like number of types of suits, number of cards, etc.
        /// </summary>
        /// <param name="deck">The deck to analyze</param>
        public void ProcessDeck(CompleteDeckData deck)
        {
            // Finding min/max strengths
            int minStrength = int.MaxValue;
            int maxStrength = int.MinValue;

            foreach (var card in deck.Cards)
            {
                minStrength = Math.Min(minStrength, card.StrengthValue);
                maxStrength = Math.Max(maxStrength, card.StrengthValue);
            }

            m_MinStrength = minStrength;
            m_MaxStrength = maxStrength;

            // Counting number of types of suits
            List<CompleteCardData> copy = new List<CompleteCardData>(deck.Cards);
            int suitCount = 0;

            for (int i = 0; i < copy.Count; i++)
            {
                if (copy[i] == null)
                {
                    continue;
                }

                Suit suit = copy[i].Suit1;
                suitCount++;

                // Removing cards that have the same suit
                for (int j = i; j < copy.Count; j++)
                {
                    if (copy[j] == null)
                    {
                        continue;
                    }

                    if (copy[j].Suit1 == suit)
                    {
                        copy[j] = null;
                    }
                }
            }

            m_NumSuits = suitCount;
        }

        public void AnalyzeCommons(List<CompleteCardData> commonPool)
        {
            CommonsStrengthCount = new List<int>(new int[m_MaxStrength - m_MinStrength + 1]);
            CommonsSuitToCountDict = new Dictionary<Suit, int>(m_NumSuits);

            // Count commons
            foreach (CompleteCardData cardData in commonPool)
            {
                CommonsStrengthCount[cardData.StrengthValue - m_MinStrength]++;
                if (CommonsSuitToCountDict.ContainsKey(cardData.Suit1))
                {
                    CommonsSuitToCountDict[cardData.Suit1]++;
                }
                else
                {
                    CommonsSuitToCountDict[cardData.Suit1] = 1;
                }
            }

            this.m_CommonPool = commonPool;
        }

        public Pattern AnalyzeHandWithCommons(List<CompleteCardData> hand)
        {
            bool pair1 = false;
            bool pair2 = false;
            bool trips = false;
            bool quads = false;

            bool flush = false;
            Suit flushSuit = null;

            bool straight = false;
            Suit straightSuit = null;
            int straightValue = -1;

            bool straightFlush = false;
            int straightFlushValue = -1;

            bool royalFlush = false;

            List<CompleteCardData> consolidated = new List<CompleteCardData>(m_CommonPool);

            foreach (var card in hand)
            {
                consolidated.Add(card);
            }

            List<int> overallStrengthCount = new List<int>(CommonsStrengthCount); // the number of cards of said strength
            Dictionary<Suit, int> overallSuitToCountDict = new Dictionary<Suit, int>(CommonsSuitToCountDict);

            // analyzing hand for card count for each suit and value.
            foreach (CompleteCardData cardData in hand)
            {
                overallStrengthCount[cardData.StrengthValue - m_MinStrength]++;
                if (overallSuitToCountDict.ContainsKey(cardData.Suit1))
                {
                    overallSuitToCountDict[cardData.Suit1]++;
                }
                else
                {
                    overallSuitToCountDict[cardData.Suit1] = 1;
                }
            }

            // matching patterns
            for (int i = 0; i < overallStrengthCount.Count; i++)
            {
                switch (overallStrengthCount[i])
                {
                    case 2:
                        if (pair1)
                        {
                            pair2 = true;
                        }
                        else
                        {
                            pair1 = true;
                        }
                        break;
                    case 3:
                        trips = true;
                        break;
                    case 4:
                        quads = true;
                        break;
                }
            }

            // detecting flushes
            foreach (var kv in overallSuitToCountDict)
            {
                if (kv.Value >= 5)
                {
                    flush = true;
                    flushSuit = kv.Key;
                    break;
                }
            }

            // detecting straights and straight flushes
            // Here, we check for a straight for every card by assuming that the card is the first in the pattern.
            // todo: Find a better solution for this.
            foreach (var card in consolidated)
            {
                int startingValue = card.StrengthValue;
                Suit startingSuit = card.Suit1;

                bool foundStraight = true;

                // start from aces
                if (startingValue == m_MaxStrength)
                {
                    startingValue = m_MinStrength - 1;
                }

                if (startingValue + 4 > m_MaxStrength)
                {
                    // overflow. not possible to have a straight with the given starting value.
                    continue;
                }

                for (int offset = 1; offset < 5; offset++)
                {
                    int desiredValue = startingValue + offset;
                    int index = desiredValue - m_MinStrength;

                    if (overallStrengthCount[index] == 0)
                    {
                        foundStraight = false;
                        break;
                    }
                }

                // if straight is found AND the startingValue is higher than the current one (a stronger straight)
                if (foundStraight && startingValue > straightValue)
                {
                    straight = true;
                    straightSuit = startingSuit;
                    straightValue = startingValue;
                }
            }

            // rare, but there's a chance that there is a straight flush
            // Brute force looking for a straight flush
            // todo: detect a royal flush
            if (straight && flush)
            {
                int suitCardCount = 0;
                if (overallSuitToCountDict.TryGetValue(straightSuit, out suitCardCount))
                {
                    if (suitCardCount >= 5)
                    {
                        // sort in increasing strength value
                        consolidated.Sort((c1, c2) => c1.StrengthValue.CompareTo(c2.StrengthValue));

                        consolidated.RemoveAll(card => card.Suit1 != straightSuit);

                        int startingIndex = 1;
                        int currentVal = consolidated[0].StrengthValue;

                        // Special case: Aces (or highest card that needs to roll back)
                        // Check for Ace being used as the smallest card first.
                        // highest card being treated as lowest card.
                        if (straightValue == m_MinStrength - 1) // is highest value
                        {
                            currentVal = m_MinStrength - 1;
                            startingIndex = 0;
                        }

                        // normal case.
                        // We go from first card to the last, and check that they are in a straight sequence
                        int straightPosition = 0;
                        for (int i = startingIndex; i < consolidated.Count; i++)
                        {
                            CompleteCardData next = consolidated[i];
                            if (next.StrengthValue - currentVal != 1)
                            {
                                currentVal = next.StrengthValue;
                                straightPosition = 0;
                                continue; // NOT A STRAIGHT, reset.
                            }

                            currentVal = next.StrengthValue;
                            straightPosition++;

                            if (straightPosition >= 4) // straight flush found!
                            {
                                straightFlush = true;
                                straightFlushValue = currentVal; // the last card.
                            }
                        }
                    }
                }
            }

            if (straightFlush && straightFlushValue == m_MaxStrength)
            {
                royalFlush = true;
            }

            if (royalFlush)
            {
                return Pattern.RoyalFlush;
            }
            else if (straightFlush)
            {
                return Pattern.StraightFlush;
            }
            else if (quads)
            {
                return Pattern.FourOfAKind;
            }
            else if (trips && pair1)
            {
                return Pattern.FullHouse;
            }
            else if (flush)
            {
                return Pattern.Flush;
            }
            else if (straight)
            {
                return Pattern.Straight;
            }
            else if (trips)
            {
                return Pattern.ThreeOfAKind;
            }
            else if (pair2)
            {
                return Pattern.TwoPair;
            }
            else if (pair1)
            {
                return Pattern.OnePair;
            }
            else
            {
                return Pattern.None;
            }
        }

        public string GetPatternString(Pattern pattern)
        {
            switch (pattern)
            {
                case Pattern.None:
                    return "High_Card";
                case Pattern.OnePair:
                    return "One Pair";
                case Pattern.TwoPair:
                    return "Two Pair";
                case Pattern.ThreeOfAKind:
                    return "Three of a Kind";
                case Pattern.Straight:
                    return "Straight";
                case Pattern.Flush:
                    return "Flush";
                case Pattern.FullHouse:
                    return "Full House";
                case Pattern.FourOfAKind:
                    return "Four of a Kind";
                case Pattern.StraightFlush:
                    return "Straight Flush";
                case Pattern.RoyalFlush:
                    return "Royal Flush";
                default:
                    return "INVALID_PATTERN_STRING";
            }
        }
    }
}
