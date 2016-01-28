using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mvvmgeo
{
    public class AppStatus : ObservableObject
    {
        private static AppStatus _instance;
        private AppStatus() { }
        public static AppStatus Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AppStatus();
                }
                return _instance;
            }
        }

        #region Private fields
        private string _newestMessage;
        #endregion

        #region Public properties
        public string AppVersion
        {
            get { return Application.ProductVersion; }
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
