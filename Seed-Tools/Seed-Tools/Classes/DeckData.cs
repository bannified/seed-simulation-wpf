using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seed_Tools
{
    public class DeckData
    {
        public DeckData()
        {
            DisplayName = "default";
            CardIdToCount = new Dictionary<string, int>();
        }

        public DeckData(string displayName) : base()
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
        public Dictionary<string, int> CardIdToCount { get; set; }

        public static void CreateDefaultDeckDataAtPath(string path, string defaultName = "default")
        {
            System.IO.FileStream fs = System.IO.File.Create(path);
            fs.Close();
            string resultJson = JsonConvert.SerializeObject(new DeckData(defaultName));
            System.IO.File.WriteAllText(path, resultJson);
        }
    }
}
