using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;

namespace mvvmgeo
{
    public class BatchGEOModelView : ObservableObject
    {
        #region Private fields
        private ObservableCollection<GEO> _geoFiles;
        private List<CSVEntry> _csvEntries;
        private GEOModelView _gmv;
        private string _folderPath;
        private string _csvFileName;
        private string[] _csvFileContents;
        private ICommand _loadFolderCommand;
        private ICommand _loadCSVCommand;
        private ICommand _startBatchProcessCommand;
        private ICommand _clearFileCommand;
        #endregion

        #region Public Properties / Commands
        public ObservableCollection<GEO> GEOFiles
        {
            get { return _geoFiles; }
            set
            {
                if(value != _geoFiles)
                {
                    _geoFiles = value;
                }
            }
        }

        public List<CSVEntry> CSVEntries
        {
            get { return _csvEntries; }
            internal set
            {
                if (value != _csvEntries)
                    _csvEntries = value;
            }
        }

        public GEOModelView GMV
        {
            get { return _gmv; }
            set
            {
                if(value != null)
                {
                    _gmv = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FolderPath
        {
            get { return _folderPath; }
            set
            {
                if(value != _folderPath)
                {
                    _folderPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CSVFileName
        {
            get { return _csvFileName; }
            set
            {
                if(value != null)
                {
                    _csvFileName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string[] CSVFileContents
        {
            get { return _csvFileContents; }
            set
            {
                if(value != null)
                {
                    _csvFileContents = value;
                }
            }
        }

        public string PIDHeader
        {
            get { return Settings.Instance.CustomProductIDLabel; }
        }
        public string DWNHeader
        {
            get { return Settings.Instance.CustomDrawingNoteLabel; }
        }
        public string CSTHeader
        {
            get { return Settings.Instance.CustomCustomerNumLabel; }
        }

        public ICommand LoadFolderCommand
        {
            get
            {
                if(_loadFolderCommand == null)
                {
                    _loadFolderCommand = new RelayCommand(
                        param => LoadFolder());
                }
                return _loadFolderCommand;
            }
        }

        public ICommand LoadCSVCommand
        {
            get
            {
                if(_loadCSVCommand == null)
                {
                    _loadCSVCommand = new RelayCommand(
                        param => LoadCSV());
                }
                return _loadCSVCommand;
            }
        }

        public ICommand StartBatchProcessCommand
        {
            get
            {
                if(_startBatchProcessCommand == null)
                {
                    _startBatchProcessCommand = new RelayCommand(
                        param => StartBatchProcess());
                }
                return _startBatchProcessCommand;
            }
        }

        public ICommand ClearFileCommand
        {
            get
            {
                if(_clearFileCommand == null)
                {
                    _clearFileCommand = new RelayCommand(
                        param => ClearFile());
                }
                return _clearFileCommand;
            }
        }
        #endregion

        #region Constructor

        public BatchGEOModelView()
        {
            GMV = new GEOModelView();
            GEOFiles = new ObservableCollection<GEO>();
            CSVEntries = new List<CSVEntry>();
        }

        #endregion

        #region Methods / Helpers / Commands

        private void LoadFolder()
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                GEOFiles.Clear();
                string[] files;
                files = Directory.GetFiles(fb.SelectedPath, "*.GEO", SearchOption.TopDirectoryOnly);
                FolderPath = fb.SelectedPath;
                foreach (var f in files)
                {
                    GMV.LoadFile(f);
                    GEOFiles.Add(GMV.CurrentFile);
                }
                AppStatus.Instance.Message("Loaded " + GEOFiles.Count + " files");
            }
            else
            {
                // show error I guess

            }
        }

        private void LoadCSV()
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.DefaultExt = ".csv";
            fd.Filter = "CSV Files (*.csv)|*.csv";
            bool? result = fd.ShowDialog();
            if (result == true)
            {
                CSVFileName = fd.FileName;
                CSVFileContents = File.ReadAllLines(fd.FileName);
                ProcessCSV();
                AppStatus.Instance.Message("CSV file loaded and processed");
            }
        }

        void ProcessCSV()
        {
            if (CSVFileName != null && CSVFileContents.Length > 0)
            {
                if(CSVEntries != null)
                    CSVEntries.Clear();
                foreach (string l in CSVFileContents)
                {
                    var spl = l.Split(new string[] { "," }, StringSplitOptions.None);
                    var fn = spl[0];
                    var pid = spl[1];
                    var dwgn = spl[2];
                    var cust = spl[3];
                    CSVEntry e = new CSVEntry(fn, pid, dwgn, cust);
                    CSVEntries.Add(e);
                }
            }
        }

        private void StartBatchProcess()
        {
            if (CSVFileName != null)
            {
                foreach (CSVEntry c in CSVEntries)
                {
                    foreach (GEO g in GEOFiles)
                    {
                        if (g.FileName.Contains(c.FileName))
                        {
                            // found a match
                            GMV.CurrentFile = g;
                            g.ProductID = c.FileName;
                            g.DrawingNote = c.DrawingNote;
                            g.CustomerNum = c.CustomerNum;
                            g.BatchDone = true;
                            GMV.SaveFile();
                        }
                    }
                }
                AppStatus.Instance.Message("Batch process completed.");
            }
        }

        private void ClearFile()
        {
            FolderPath = null;
            GEOFiles.Clear();
            CSVEntries.Clear();
            CSVFileContents = null;
            CSVFileName = "";
            GMV = null;
        }
        #endregion
    }
}
