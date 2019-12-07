using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class DeckEntryData
    {
        public CardData Card;
        public int Count;

        public DeckEntryData(CardData data, int count)
        {
            this.Card = data;
            this.Count = count;
        }
    }
}
