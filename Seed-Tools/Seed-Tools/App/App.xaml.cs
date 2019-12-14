using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Seed_Tools
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public Random random = new Random();

        public Dictionary<string, CardData> CardLibrary;

        public static App CastedInstance { get { return Current as App; } }

        public List<Suit> SuitsCollection { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // load user settings
            Seed_Tools.Properties.Settings.Default.Reload();

            CheckAndValidateData();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Save properties
            SaveCurrentCardLibrary();

            Seed_Tools.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Gets the assembly's path.
        /// </summary>
        /// <returns>Root path of app</returns>
        public static string GetRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Returns the relative path given its full path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>A file path relative to the assembly's path.</returns>
        public static string GetRelativePathFromFullPath(string fullPath)
        {
            return fullPath.Replace(GetRootPath(), ".");
        }

        /// <summary>
        /// Checks and validates all data. Creates defaults if anything is missing.
        /// </summary>
        void CheckAndValidateData()
        {
            if (!System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.ActiveDeckPath))
            {
                // Create Default Deck.
                DeckData.CreateDefaultDeckDataAtPath(Seed_Tools.Properties.Settings.Default.ActiveDeckPath);
            }

            LoadCurrentCardLibrary();

            foreach (var kv in CardLibrary)
            {
                kv.Value.id = kv.Key;
            }
        }

        /// <summary>
        /// Loads the card library at the default card library path.
        /// If there's no card library, then a blank one is made.
        /// </summary>
        public void LoadCurrentCardLibrary()
        {
            string path = Seed_Tools.Properties.Settings.Default.CardLibraryPath;
            if (!System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.CardLibraryPath))
            {
                CardLibrary = new Dictionary<string, CardData>();
            } else
            {
                Dictionary<string, CardData> data = JsonConvert.DeserializeObject< Dictionary<string, CardData> >(System.IO.File.ReadAllText(path));
                CardLibrary = data;
            }
        }

        /// <summary>
        /// Saves the current card library to the default path.
        /// </summary>
        public void SaveCurrentCardLibrary()
        {
            string path = Seed_Tools.Properties.Settings.Default.CardLibraryPath;
            System.IO.FileStream fs = System.IO.File.Create(path);
            fs.Close();

            string resultJson = JsonConvert.SerializeObject(CardLibrary);
            System.IO.File.WriteAllText(path, resultJson);
        }
	}
}
