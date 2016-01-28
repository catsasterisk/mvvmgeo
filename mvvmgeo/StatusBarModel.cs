using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmgeo
{
    public class StatusBarModel : ObservableObject
    {
        private static StatusBarModel _instance;
        private StatusBarModel() { }
        public static StatusBarModel Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new StatusBarModel();
                }
                return _instance;
            }
        }

        #region Private fields
        private const string _appVersion = "v0.1.26";
        private string _newestMessage;
        #endregion

        #region Public properties
        public string AppVersion
        {
            get { return _appVersion; }
        }
        public string NewestMessage
        {
            get { return _newestMessage; }
            set
            {
                if(value != _newestMessage)
                {
                    _newestMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public void StatusBarMessage(string msg)
        {
            NewestMessage = msg;
        }
    }
}
