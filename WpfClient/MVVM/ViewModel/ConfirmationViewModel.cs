using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfClient.Idioms;
using WpfClient.Utilities;
using WpfClient.Utilities.Enum;

namespace WpfClient.MVVM.ViewModel
{
    public class ConfirmationViewModel : INotifyPropertyChanged, ICloseable
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
        public ICommand CancelCommand { get; set; }

        public event Action<bool?> CloseRequested;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ConfirmationViewModel(TextKeys tittle, TextKeys message, ConfirmationIconType iconType)
        {
            Message = Language.GetLocalizedString(tittle) ;
            Tittle = Language.GetLocalizedString(message) ;
            ImageIcon = PathsIcons.GetIconPath(iconType);
            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(false));
        }
        public ConfirmationViewModel(string tittle, string message, ConfirmationIconType iconType)
        {
            Message = tittle;
            Tittle = message;
            ImageIcon = PathsIcons.GetIconPath(iconType);
            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(false));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
