﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed_Tools
{
    public class SeedData
    {
        public Dictionary<string, CardData> AllCards { get; set; } = new Dictionary<string, CardData>();

        public List<Suit> Suits { get; set; } = new List<Suit>();
    }
}
