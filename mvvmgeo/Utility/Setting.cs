using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmgeo.Utility
{
    class Setting : ObservableObject
    {
        #region Private fields
        private string _name;
        private bool _value;
        private bool _default;
        #endregion

        #region Public properties
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Value
        {
            get { return _value; }
            set
            {
                if(_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Default
        {
            get { return _default; }
            internal set
            {
                _default = value;
            }
        }
        #endregion

        public Setting(string name, bool @default)
        {
            Name = name;
            Value = @default;
            Default = @default;
        }
    }
}
