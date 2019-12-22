using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    /// <summary>
    /// A Complete deck that's more fit for use than DeckData
    /// by resolving indirect data of DeckData such as Cards and its Suit.
    /// </summary>
    public class CompleteDeckData
    {
        public Queue<CompleteCardData> Cards;

        public CompleteDeckData(CompleteDeckData toCopy)
        {
            Cards = new Queue<CompleteCardData>(toCopy.Cards);
        }

        public CompleteDeckData(List<CompleteCardData> toCopy)
        {
            Cards = new Queue<CompleteCardData>(toCopy);
        }

        public CompleteDeckData(DeckData inputDeck)
        {
            Dictionary<string, int> cards = new Dictionary<string, int>(inputDeck.CardIdToCount);

            foreach (var kv in cards)
            {
                int cardCount = kv.Value;
                CardData simpleCard = null;
                if (!App.CastedInstance.CardLibrary.TryGetValue(kv.Key, out simpleCard))
                {
                    continue;
                }

                CompleteCardData completeCard = new CompleteCardData(simpleCard);

                for (int i = 0; i < cardCount; i++)
                {
                    Cards.Enqueue(completeCard);
                }
            }
        }

    }
}
