using System.Windows.Controls;
using WpfClient.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace WpfClient.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para SideBarView.xaml
    /// </summary>
    public partial class SideBarView : UserControl
    {
        public SideBarView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<SideBarViewModel>();
        }
    }
}
