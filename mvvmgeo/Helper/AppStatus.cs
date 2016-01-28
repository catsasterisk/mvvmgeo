using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace mvvmgeo
{
    public class AppStatus : ObservableObject
    {
        #region Singleton
        private static AppStatus _instance;
        private AppStatus()
        {
            Messages = new ObservableCollection<string>();
        }
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
        #endregion

        #region Private fields
        private string _newestMessage;
        private ObservableCollection<string> _messages;
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

        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set
            {
                if(value != null)
                {
                    _messages = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public void Message(string msg)
        {
            NewestMessage = "(" + DateTime.Now.ToString("HH:mm:ss") + ") Msg: " + msg;
            Messages.Add(NewestMessage);
        }
    }
}
