using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class AlertViewModel : INotifyPropertyChanged
    {
        private string _message;
        private string _tittle;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Tittle
        {
            get => _tittle;
            set
            {
                _tittle = value;
                OnPropertyChanged(nameof(Tittle));
            }
        }
        public ICommand ConfirmCommand { get; set; }
        public List<string> _validationErrors;

        public event Action<bool?> CloseRequested;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
