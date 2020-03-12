using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public abstract class GameRulesBase
    {
        public abstract class ResultBase : IComparable<ResultBase>
        {
            public abstract int CompareTo(ResultBase other);
        }

        /// <summary>
        /// Processes the deck for metadata, like number of types of suits, number of cards, etc.
        /// </summary>
        /// <param name="deck">The deck to analyze</param>
        protected virtual void ProcessDeck(CompleteDeckData deck) { }

        private GameRulesBase() { }

        public GameRulesBase(CompleteDeckData deck)
        {
            ProcessDeck(deck);
        }

        public abstract void AnalyzeCommons(List<CompleteCardData> commonPool);

        public abstract ResultBase AnalyzeHandWithCommons(List<CompleteCardData> hand);

    }
}
