﻿using System;
using System.Windows;

namespace mvvmgeo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Bind appstatus class to the main status bar
            StatusBar.DataContext = AppStatus.Instance;

            // Set up single file editor
            GEOModelView gmv = new GEOModelView();
            Grid_Editor.DataContext = gmv;

            // set up batch file editor
            BatchGEOModelView bmv = new BatchGEOModelView();
            Grid_Batch.DataContext = bmv;

            // check to see if app opens with geo file command line args
            if (Environment.GetCommandLineArgs().Length > 1)
                gmv.LoadFile(Environment.GetCommandLineArgs()[1]);
        }
    }
}
