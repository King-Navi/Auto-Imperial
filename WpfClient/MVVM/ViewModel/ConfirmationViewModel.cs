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
    public class ConfirmationViewModel : INotifyPropertyChanged
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
        public ICommand CancelCommand { get; set; }

        public event Action<bool?> CloseRequested;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ConfirmationViewModel(string tittle,string message)
        {
            Message = message;
            Tittle = tittle;
            ConfirmCommand = new RelayCommand(o => CloseRequested?.Invoke(true));
            CancelCommand = new RelayCommand(o => CloseRequested?.Invoke(false));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
