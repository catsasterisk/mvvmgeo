using System.Windows;

namespace mvvmgeo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set up single file editor
            GEOModelView gmv = new GEOModelView();
            Grid_Editor.DataContext = gmv;

            // set up batch file editor
            BatchGEOModelView bmv = new BatchGEOModelView();
            //Grid_BatchFile.DataContext = bmv;

            ReplaceGEOModelView rmv = new ReplaceGEOModelView();
            //Grid_Replace.DataContext = rmv;

            Settings s = new Settings();
            //Grid_Settings.DataContext = s;
        }
    }
}
