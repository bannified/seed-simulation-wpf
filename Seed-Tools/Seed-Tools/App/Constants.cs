using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Seed_Tools
{
    public partial class App : Application
    {
        /// <summary>
        /// All pre-defined constants for Seed Tools
        /// </summary>
        public partial class Constants
        {
            public partial class SimulationParameters
            {
                public static readonly int MINIMUM_NUM_OF_PLAYERS = 2;
            }

            public partial class FileExtensions
            {
                public static readonly string DECK = ".dek";
                public static readonly string CARD_LIBRARY = ".clb";
            }

            /// <summary>
            /// Common directories that this app uses
            /// </summary>
            public partial class Paths
            {
                public static readonly string CARD_IMAGES_PATH = "./images/";
            }

            /// <summary>
            /// Common File Dialog Filters
            /// </summary>
            public partial class FileDialogFilters
            {
                public static readonly string GRAPHICS_FILTER = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff" + "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|";
            }

            public partial class Scaling
            {
                public static readonly double CardHeightToWidthRatio = 1.468;
                public static readonly double CardWidthToHeightRatio = 1.0/1.468;
            }
        }
    }
}
