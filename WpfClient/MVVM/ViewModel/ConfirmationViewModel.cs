using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class ConfirmationViewModel : INotifyPropertyChanged
    {
        public string Message { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public event Action<bool?> CloseRequested;

        public ConfirmationViewModel(string message)
        {
            Message = message;

            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(false));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
