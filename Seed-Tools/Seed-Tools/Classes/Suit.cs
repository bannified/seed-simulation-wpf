using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class Suit
    {
        public Suit() { }

        public Suit(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; } = "New Suit";

        // Value used to comparing suits (set all to 0 if they're all equal)
        public int Value { get; set; } = 0;

        // Main image associated with this suit
        public string ImagePath { get; set; } = "";
    }
}
