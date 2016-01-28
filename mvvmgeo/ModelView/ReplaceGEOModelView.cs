using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mvvmgeo
{
    class ReplaceGEOModelView : ObservableObject
    {
        #region Private fields
        private GEO _oldGEOFile;
        private GEO _newGEOFile;
        private GEOModelView _gmv;
        private ICommand _loadOldGEOCommand;
        private ICommand _loadNewGEOCommand;
        private ICommand _clearFieldsCommand;
        #endregion

        #region Public Properties
        public GEO OldGEOFile
        {
            get { return _oldGEOFile; }
            set
            {
                if(value != null)
                {
                    _oldGEOFile = value;
                    OnPropertyChanged();
                }
            }
        }
        public GEO NewGEOFile
        {
            get { return _newGEOFile; }
            set
            {
                if(value != null)
                {
                    _newGEOFile = value;
                    OnPropertyChanged();
                }
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
        public ICommand LoadOldGEOCommand
        {
            get
            {
                if(_loadOldGEOCommand == null)
                {
                    _loadOldGEOCommand = new RelayCommand(
                        async param => await LoadGEO(true));
                }
                return _loadOldGEOCommand;
            }
        }
        public ICommand LoadNewGEOCommand
        {
            get
            {
                if(_loadNewGEOCommand == null)
                {
                    _loadNewGEOCommand = new RelayCommand(
                        async param => await LoadGEO(false));
                }
                return _loadNewGEOCommand;
            }
        }
        public ICommand ClearFieldsCommand
        {
            get
            {
                if(_clearFieldsCommand == null)
                {
                    _clearFieldsCommand = new RelayCommand(
                        param => ClearFields());
                }
                return _clearFieldsCommand;
            }
        }
        #endregion

        #region Constructor
        public ReplaceGEOModelView()
        {
            GMV = new GEOModelView();
        }
        #endregion

        #region Methods
        private async Task LoadGEO(bool oldFile)
        {
            await GMV.LoadFile();
            if(oldFile)
            {
                OldGEOFile = GMV.CurrentFile;
            }
            else
            {
                NewGEOFile = GMV.CurrentFile;
                if(OldGEOFile != null)
                {
                    NewGEOFile.ProductID = OldGEOFile.ProductID;
                    NewGEOFile.DrawingNote = OldGEOFile.DrawingNote;
                    NewGEOFile.CustomerNum = OldGEOFile.CustomerNum;
                }
            }
        }
        private void ClearFields()
        {
            System.Windows.MessageBox.Show("Clearing Fields");
            OldGEOFile = null;
            NewGEOFile = null;
        }
        #endregion
    }
}
