using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ViewModels.utilities
{
    public abstract class clsVMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void showError()
        {
            ContentDialog error = new ContentDialog()       //TODO vista error
            {
                Content = "Ha ocurrido un error.",
                CloseButtonText = "Ok"
            };
        }
    }
}
