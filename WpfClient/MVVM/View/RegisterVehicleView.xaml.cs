using System.Windows;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para RegisterVehicleView.xaml
    /// </summary>
    public partial class RegisterVehicleView : Window
    {
        public RegisterVehicleView(RegisterVehicleViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
