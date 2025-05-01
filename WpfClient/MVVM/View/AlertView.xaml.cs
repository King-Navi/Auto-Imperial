using System.Windows;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    /// <summary>
    /// Interaction logic for AlertView.xaml
    /// </summary>
    public partial class AlertView : Window
    {
        public AlertView()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                if (DataContext is AlertViewModel vm)
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
