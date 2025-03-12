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
