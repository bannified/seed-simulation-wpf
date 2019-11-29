﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seed_Tools
{
    /// <summary>
    /// Interaction logic for CardLibraryEntryView.xaml
    /// </summary>
    public partial class CardLibraryEntryView : UserControl
    {
        public CardData card { get; set; }

        public CardLibraryEntryView(CardData data) : base()
        {
            card = data;
        }

        public CardLibraryEntryView()
        {
            InitializeComponent();
        }
    }
}
