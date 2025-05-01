using System.Windows.Input;

namespace WpfClient.Utilities
{
    public interface IRelayCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
