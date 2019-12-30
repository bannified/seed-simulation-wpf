using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class CompleteCardData
    {
        public string id { get; set; }
        public string Name { get; set; }

        // Gameplay variables
        public Suit Suit1 { get; set; }
        public int StrengthValue { get; set; } = 0;

        public CompleteCardData(CardData simpleCard)
        {
            this.id = simpleCard.id;
            this.Name = simpleCard.Name;

            Suit suit = null;
            if (!App.CastedInstance.SuitsCollection.TryGetValue(simpleCard.Suit1, out suit))
            {
                suit = new Suit();
            }
            this.Suit1 = suit;

            this.StrengthValue = simpleCard.StrengthValue;
        }

        public CompleteCardData(Suit suit, int strength)
        {
            this.Suit1 = suit;
            this.StrengthValue = strength;
        }
    }
}
