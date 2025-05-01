using Ookii.Dialogs.Wpf;
using System.Windows;
using System.Windows.Controls;
using WpfClient.MVVM.ViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WpfClient.MVVM.View
{

    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
        }

        private void ClickButtonSelectFolder(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                if (DataContext is ReportsViewModel vm)
                {
                    vm.SelectedPath = dialog.SelectedPath;
                }
            }
        }

        private void ClickButtonGenerateReportClient(object sender, System.Windows.RoutedEventArgs e)
        {
            var inputDialog = new InputDialog("Introduzca el CURP del cliente:");
            if (inputDialog.ShowDialog() == true)
            {
                string value = inputDialog.ResponseText;
                MessageBox.Show("CURP: " + value);
                if (DataContext is ReportsViewModel vm)
                {
                    vm.CLientCURP = value;
                    vm.GenerateClientReportCommand.Execute(null);
                }
                
            }
        }
    }
}
