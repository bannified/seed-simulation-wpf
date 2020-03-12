using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class Simulation
    {
        // Parameters
        public int NumberOfCardsPerPlayer { get; set; }
        public int NumberOfCommonCards { get; set; }
        public int NumberOfSimulations { get; set; }
        public int NumberOfPlayers { get; set; }
        public CompleteDeckData Deck { get; set; }
        public GameRulesBase CurrentGameRules { get; set; }

        // Simulation runtime variables
        private int m_CurrentSimNumber = 0;
        private List<PlayerGameData> m_PlayerCards; // Cards of every player
        private List<CompleteCardData> m_ShuffleDeck;
        private List<CompleteCardData> m_CommonCards { get; set; }

        public Simulation(DeckData deckData, int numberOfCardsPerPlayer, int numberOfCommonCards, int numberOfSimulations, int numberOfPlayers)
        {
            this.NumberOfCardsPerPlayer = numberOfCardsPerPlayer;
            this.NumberOfCommonCards = numberOfCommonCards;
            this.NumberOfSimulations = numberOfSimulations;
            this.NumberOfPlayers = numberOfPlayers;

            this.m_ShuffleDeck = deckData.GetFullCompleteCardDataList();
            m_CommonCards = new List<CompleteCardData>(NumberOfCommonCards);
            m_PlayerCards = new List<PlayerGameData>(NumberOfPlayers);
            CurrentGameRules = new PokerGameRules(new CompleteDeckData(m_ShuffleDeck));
        }

        public void Run()
        {
            m_CurrentSimNumber = 0;
            
            // Initialize Players
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                m_PlayerCards.Add(new PlayerGameData());
            }

            for (int i = 0; i < NumberOfSimulations; i++)
            {
                ++m_CurrentSimNumber;
                // Setup deck, shuffle deck.
                m_ShuffleDeck.Shuffle();
                Deck = new CompleteDeckData(m_ShuffleDeck);

                // Distribute cards to players
                for (int turn = 0; turn < NumberOfCardsPerPlayer; turn++)
                {
                    foreach (PlayerGameData player in m_PlayerCards)
                    {
                        player.AddCardToHand(Deck.Cards.Dequeue());
                    }
                }

                // Add cards to common pool
                for (int drawNumber = 0; drawNumber < NumberOfCommonCards; drawNumber++)
                {
                    m_CommonCards.Add(Deck.Cards.Dequeue());
                }

                // todo: Add checking of rules for every player, and determine the winner.
                for (int player = 0; player < NumberOfPlayers; player++)
                {

                }

                ClearSimulation();
            }
        }

        private void ClearSimulation()
        {
            foreach (var player in m_PlayerCards)
            {
                player.ClearHand();
            }

            m_CommonCards.Clear();
        }
    }
}
