using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    /// <summary>
    /// The data representation for a Card.
    /// </summary>
    public class CardData
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string MainImageSourcePath { get; set; }

        // Gameplay variables
        public Suit Suit1 { get; set; } = new Suit();
        public int StrengthValue { get; set; } = 0;

        public CardData()
        {
            id = "-1";
            Name = "Invalid Card";
            MainImageSourcePath = "images/sample-card.png";
        }

        public CardData(string id) : this()
        {
            this.id = id;
        }
    }
}
