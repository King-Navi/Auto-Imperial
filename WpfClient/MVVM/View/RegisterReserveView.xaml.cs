using System.Windows;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    public partial class RegisterReserveView : Window
    {
        public RegisterReserveView()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                if (DataContext is RegisterReserveViewModel vm)
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
