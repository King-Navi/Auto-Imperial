using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class AlertViewModel : INotifyPropertyChanged
    {
        public string Message { get; set; }
        public RelayCommand ConfirmCommand { get; set; }
        public List<string> _validationErrors;

        public event Action<bool?> CloseRequested;
        public event PropertyChangedEventHandler PropertyChanged;

        public AlertViewModel(string message)
        {
            Message = message;
            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
        }

        public AlertViewModel(string message, List<string> validationErrors) : this(message)
        {
            _validationErrors = validationErrors;
            if (_validationErrors.Count > 0)
            {
                foreach (var error in _validationErrors)
                {
                    Message += error;
                }
            }
        }

    }
}
