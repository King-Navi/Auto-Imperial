using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para AdminSideBarView.xaml
    /// </summary>
    public partial class AdminSideBarView : UserControl
    {
        public AdminSideBarView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<AdminSideBarViewModel>();
        }
    }
}
