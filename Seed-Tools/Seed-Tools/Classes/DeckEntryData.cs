using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class DeckEntryData
    {
        public CardData Card { get; set; }
        public int Count { get; set; }

        public DeckEntryData(CardData data, int count)
        {
            this.Card = data;
            this.Count = count;
        }
    }
}
