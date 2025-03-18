using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfClient.Utilities;
using WpfClient.Utilities.Enum;

namespace WpfClient.MVVM.ViewModel
{
    public class AlertViewModel : INotifyPropertyChanged
    {
        private string _message;
        private string _tittle;
        private string _imageSource;

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
        public string ImageIcon
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();
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

        public AlertViewModel(string tittle ,string message, AlertIconType iconType)
        {
            Tittle = tittle;
            Message = message;
            ImageIcon = PathsIcons.GetIconPath(iconType);
            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
        }

        public AlertViewModel(string tittle, string message, AlertIconType iconType, List<string> validationErrors) : this(message, tittle, iconType)
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
