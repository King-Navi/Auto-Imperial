using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
