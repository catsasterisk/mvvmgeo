using System.IO;

namespace mvvmgeo
{
    public class GEO : ObservableObject
    {
        #region Private fields
        private string _fileName;
        private string[] _fileContents;
        private string _productID;
        private string _drawingNote;
        private string _customerNum;
        private bool _batchDone;
        #endregion

        #region Public Properties
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if(value != _fileName)
                {
                    _fileName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ShortFileName
        {
            get { return Path.GetFileName(FileName); }
        }

        public string[] FileContents
        {
            get { return _fileContents; }
            set
            {
                if(value != null)
                {
                    _fileContents = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ProductID
        {
            get { return _productID; }
            set
            {
                if(value != null)
                {
                    _productID = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DrawingNote
        {
            get { return _drawingNote; }
            set
            {
                if(value != null)
                {
                    _drawingNote = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CustomerNum
        {
            get { return _customerNum; }
            set
            {
                if(value != null)
                {
                    _customerNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool BatchDone
        {
            get { return _batchDone; }
            set
            {
                if(value != _batchDone)
                {
                    _batchDone = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public GEO()
        {
            BatchDone = false;
        }
    }
}
