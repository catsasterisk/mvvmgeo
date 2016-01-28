using System;
using System.Text;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;

namespace mvvmgeo
{
    public class GEOModelView : ObservableObject
    {
        #region Private Constants
        private const string productIdString = "IDENT@";
        private const string drawingNoteString = "BEZCH@";
        private const string customerNumString = "TKUND@";
        private const string propertySectionEndString = "#~TTINFO_END";
        #endregion

        #region Private Fields
        private GEO _currentFile;
        private ICommand _loadFileCommand;
        private ICommand _saveFileCommand;
        private ICommand _saveAsFileCommand;
        private ICommand _clearFileCommand;
        #endregion

        #region Public Properties / Commands
        public AppStatus SBM;
        public GEO CurrentFile
        {
            get { return _currentFile; }
            set
            {
                if(value != _currentFile)
                {
                    _currentFile = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoadFileCommand
        {
            get
            {
                if (_loadFileCommand == null)
                {
                    _loadFileCommand = new RelayCommand(
                        param => LoadFile());
                }
                return _loadFileCommand;
            }
        }

        public ICommand SaveFileCommand
        {
            get
            {
                if(_saveFileCommand == null)
                {
                    _saveFileCommand = new RelayCommand(
                        param => SaveFile());
                }
                return _saveFileCommand;
            }
        }

        public ICommand SaveAsFileCommand
        {
            get
            {
                if(_saveAsFileCommand == null)
                {
                    _saveAsFileCommand = new RelayCommand(
                        param => SaveFile(true));
                }
                return _saveAsFileCommand;
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

        #region Methods / Helpers
        public void LoadFile(string f = "")
        {
            // check if file is already loaded
            if(CurrentFile != null)
            {
                // ask for save?
                //MessageBox.Show("Save current file?");
            }
            if (f == "")
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.DefaultExt = ".GEO";
                fd.Filter = "GEO Files (*.GEO)|*.GEO";
                bool? result = fd.ShowDialog();
                if (result == true)
                {
                    // wrap in file open dialog
                    if (File.Exists(fd.FileName))
                    {
                        // wrap in geo checking
                        GEO g = new GEO();
                        g.FileName = fd.FileName;
                        g.FileContents = File.ReadAllLines(fd.FileName);
                        CurrentFile = g;
                        GetValues();
                        AppStatus.Instance.Message("File loaded successfully");
                    }
                }
            }
            else
            {
                // wrap in file open dialog
                if (File.Exists(f))
                {
                    // wrap in geo checking
                    GEO g = new GEO();
                    g.FileName = f;
                    g.FileContents = File.ReadAllLines(f);
                    CurrentFile = g;
                    GetValues();
                }
            }

        }

        public void SaveFile(bool saveAs = false)
        {
            if (CurrentFile != null)
            {
                bool matchPid = false;
                bool matchDwgn = false;
                bool matchCust = false;

                StringBuilder newFile = new StringBuilder();
                int lineNum = 0;
                foreach (string line in CurrentFile.FileContents)
                {
                    lineNum++;
                    // drawing note
                    if (line.Contains(drawingNoteString))
                    {
                        matchDwgn = true;
                        newFile.Append(drawingNoteString + CurrentFile.DrawingNote + "\r\n");
                        continue;
                    }
                    // part id
                    if (line.Contains(productIdString))
                    {
                        matchPid = true;
                        newFile.Append(productIdString + CurrentFile.ProductID + "\r\n");
                        continue;
                    }
                    // customer num
                    if (line.Contains(customerNumString))
                    {
                        matchCust = true;
                        newFile.Append(customerNumString + CurrentFile.CustomerNum + "\r\n");
                        continue;
                    }
                    // check for end of properties
                    if (CurrentFile.FileContents.Length != lineNum)
                    {
                        if (line == propertySectionEndString)
                        {
                            if (CurrentFile.ProductID != "" && matchPid == false)
                            {
                                newFile.Append(productIdString + CurrentFile.ProductID + "\r\n");
                            }
                            if (CurrentFile.DrawingNote != "" && matchDwgn == false)
                            {
                                newFile.Append(drawingNoteString + CurrentFile.DrawingNote + "\r\n");
                            }
                            if (CurrentFile.CustomerNum != "" && matchCust == false)
                            {
                                newFile.Append(customerNumString + CurrentFile.CustomerNum + "\r\n");
                            }
                            newFile.Append(propertySectionEndString + "\r\n");
                            continue;
                        }
                    }
                    newFile.Append(line + "\r\n");
                }
                File.WriteAllText(CurrentFile.FileName, newFile.ToString());
                AppStatus.Instance.Message("File saved successfully");
            }
        }

        private void GetValues()
        {
            string[] split;
            foreach (var line in CurrentFile.FileContents)
            {
                // drawing note
                if (line.Contains(drawingNoteString))
                {
                    split = line.Split(new string[] { "@" }, StringSplitOptions.None);
                    CurrentFile.DrawingNote = split[1];
                }
                // part id
                if (line.Contains(productIdString))
                {
                    split = line.Split(new string[] { "@" }, StringSplitOptions.None);
                    CurrentFile.ProductID = split[1];
                }
                // customer num
                if (line.Contains(customerNumString))
                {
                    split = line.Split(new string[] { "@" }, StringSplitOptions.None);
                    CurrentFile.CustomerNum = split[1];
                }
            }
        }

        private void ClearFile()
        {
            CurrentFile = null;
        }
        #endregion
    }
}
