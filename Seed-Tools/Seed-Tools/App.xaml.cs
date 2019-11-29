using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;
using Json.Net;

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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // load user settings
            Seed_Tools.Properties.Settings.Default.Reload();

            CheckLibraries();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Save properties
            SaveCurrentCardLibrary();

            Seed_Tools.Properties.Settings.Default.Save();
        }

        public static string GetRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string GetRelativePathFromFullPath(string fullPath)
        {
            return fullPath.Replace(GetRootPath(), ".");
        }

        void CheckLibraries()
        {
            if (!System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.ActiveDeckPath))
            {
                // Create Default Deck.
                DeckData.CreateDefaultDeckDataAtPath(Seed_Tools.Properties.Settings.Default.ActiveDeckPath);
            }

            LoadCurrentCardLibrary();
        }

        public void LoadCurrentCardLibrary()
        {
            string path = Seed_Tools.Properties.Settings.Default.CardLibraryPath;
            if (!System.IO.File.Exists(Seed_Tools.Properties.Settings.Default.CardLibraryPath))
            {
                CardLibrary = new Dictionary<string, CardData>();
            } else
            {
                Dictionary<string, CardData> data = JsonNet.Deserialize< Dictionary<string, CardData> >(System.IO.File.ReadAllText(path));
                CardLibrary = data;
            }
        }

        public void SaveCurrentCardLibrary()
        {
            string path = Seed_Tools.Properties.Settings.Default.CardLibraryPath;
            System.IO.FileStream fs = System.IO.File.Create(path);
            fs.Close();
            string resultJson = JsonNet.Serialize(CardLibrary);
            System.IO.File.WriteAllText(path, resultJson);
        }
	}
}
