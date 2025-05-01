using System.Windows;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    public partial class ConfirmationView : Window
    {
        public ConfirmationView()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                if (DataContext is ConfirmationViewModel vm)
                {
                    vm.CloseRequested += result =>
                    {
                        Close();
                    };
                }
            };
        }
    }
}
