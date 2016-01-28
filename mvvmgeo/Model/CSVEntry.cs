namespace mvvmgeo
{
    public class CSVEntry
    {
        private string _fileName;
        private string _productID;
        private string _drawingNote;
        private string _customerNum;

        public string FileName
        {
            get { return _fileName; }
            internal set
            {
                if (value != _fileName)
                    _fileName = value;
            }
        }
        public string ProductID
        {
            get { return _productID; }
            internal set
            {
                if (value != _productID)
                    _productID = value;
            }
        }
        public string DrawingNote
        {
            get { return _drawingNote; }
            internal set
            {
                if (value != _drawingNote)
                    _drawingNote = value;
            }
        }
        public string CustomerNum
        {
            get { return _customerNum; }
            internal set
            {
                if (value != _customerNum)
                    _customerNum = value;
            }
        }

        public CSVEntry(string f, string p, string d, string c)
        {
            FileName = f;
            ProductID = p;
            DrawingNote = d;
            CustomerNum = c;
        }
    }
}
