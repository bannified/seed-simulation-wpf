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
        public int id { get; set; }
        public string Name { get; set; }
        public string MainImageSourcePath { get; set; }

        public CardData()
        {
            id = -1;
            Name = "Invalid Card";
            MainImageSourcePath = "images/sample-card.png";
        }
    }
}
