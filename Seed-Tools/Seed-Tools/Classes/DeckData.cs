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

        public DeckData(string displayName) : this()
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
        public Dictionary<string, int> CardIdToCount { get; set; }

        public static void CreateDefaultDeckDataAtPath(string path, string defaultName = "default")
        {
            System.IO.FileStream fs = System.IO.File.Create(path);
            fs.Close();
            string resultJson = JsonConvert.SerializeObject(new DeckData(defaultName), Formatting.Indented);
            System.IO.File.WriteAllText(path, resultJson);
        }

        public List<CompleteCardData> GetFullCompleteCardDataList()
        {
            List<CompleteCardData> resultList = new List<CompleteCardData>();

            foreach (var kv in CardIdToCount)
            {
                int cardCount = kv.Value;
                CardData simpleCard = null;
                if (!App.CastedInstance.CardLibrary.TryGetValue(kv.Key, out simpleCard))
                {
                    continue;
                }

                CompleteCardData completeCard = new CompleteCardData(simpleCard);

                for (int i = 0; i < cardCount; i++)
                {
                    resultList.Add(completeCard);
                }
            }

            return resultList;
        }
    }
}
