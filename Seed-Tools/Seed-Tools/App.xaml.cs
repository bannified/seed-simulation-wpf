using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

namespace Seed_Tools
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // load user settings
            Seed_Tools.Properties.Settings.Default.Reload();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Save properties
            Seed_Tools.Properties.Settings.Default.Save();
        }

        public static string GetRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
	}
}
