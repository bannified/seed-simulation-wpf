using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class PlayerGameData
    {
        private List<CompleteCardData> hand;



        public void ClearHand()
        {
            hand.Clear();
        }

        public void AddCardToHand(CompleteCardData card)
        {
            hand.Add(card);
        }
    }
}
