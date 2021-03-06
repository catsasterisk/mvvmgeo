﻿using System.Windows.Input;

namespace mvvmgeo
{
    class Settings : ObservableObject
    {
        #region Singleton
        private static Settings _instance;
        private Settings()
        {
            CustomProductIDLabel = Properties.Settings.Default.CustomProductID;
            CustomDrawingNoteLabel = Properties.Settings.Default.CustomDrawingNote;
            CustomCustomerNumLabel = Properties.Settings.Default.CustomCustomerNum;
        }
        public static Settings Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }
        #endregion // singleton

        #region Private Fields

        private string _customProductIDLabel;
        private string _customDrawingNoteLabel;
        private string _customCustomerNumLabel;
        private bool _saveWithoutPrompt;
        private ICommand _saveSettingsCommand;

        #endregion // Private fields

        #region Public properties
        public string CustomProductIDLabel
        {
            get { return _customProductIDLabel; }
            set
            {
                if(value != _customProductIDLabel)
                {
                    _customProductIDLabel = value;
                    Properties.Settings.Default.CustomProductID = _customProductIDLabel;
                    OnPropertyChanged();
                }
            }
        }
        public string CustomDrawingNoteLabel
        {
            get { return _customDrawingNoteLabel; }
            set
            {
                if(value != _customDrawingNoteLabel)
                {
                    _customDrawingNoteLabel = value;
                    Properties.Settings.Default.CustomDrawingNote = _customDrawingNoteLabel;
                    OnPropertyChanged();
                }
            }
        }
        public string CustomCustomerNumLabel
        {
            get { return _customCustomerNumLabel; }
            set
            {
                if(value != _customCustomerNumLabel)
                {
                    _customCustomerNumLabel = value;
                    Properties.Settings.Default.CustomCustomerNum = _customCustomerNumLabel;
                    OnPropertyChanged();
                }
            }
        }
        public bool SaveWithoutPrompt
        {
            get { return _saveWithoutPrompt; }
            set
            {
                if(value != _saveWithoutPrompt)
                {
                    _saveWithoutPrompt = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand SaveSettingsCommand
        {
            get
            {
                if(_saveSettingsCommand == null)
                {
                    _saveSettingsCommand = new RelayCommand(
                        param => Save());
                }
                return _saveSettingsCommand;
            }
        }
        #endregion // Public properties

        #region Methods
        private void Save()
        {
            Properties.Settings.Default.Save();
            AppStatus.Instance.Message("Settings saved");
        }
        #endregion // Methods
    }
}
