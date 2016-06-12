using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreatorZamowien
{
    public class Settings:INotifyPropertyChanged
    {
        private int _Amount = 1;

        public int Amount
        {
            get { return this._Amount; }
            set
            {
                if (this._Amount != value)
                {
                    this._Amount = value;
                    this.NotifyPropertyChanged("Amount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
