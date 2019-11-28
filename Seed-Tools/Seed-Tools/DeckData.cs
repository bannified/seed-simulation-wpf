using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Net;

namespace Seed_Tools
{
    public class DeckData
    {
        public DeckData()
        {
            DisplayName = "default";
            Cards = new List<CardData>();
        }

        public DeckData(string displayName) : base()
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
        public List<CardData> Cards { get; set; }

        public static void CreateDefaultDeckDataAtPath(string path, string defaultName = "default")
        {
            System.IO.FileStream fs = System.IO.File.Create(path);
            fs.Close();
            string resultJson = JsonNet.Serialize(new DeckData(defaultName));
            System.IO.File.WriteAllText(path, resultJson);
        }
    }
}
