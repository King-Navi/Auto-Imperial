using Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.MVVM.View;
using WpfClient.MVVM.ViewModel;

namespace WpfClient.Utilities
{
    public class DialogService: IDialogService
    {
        public bool? ShowDialog(object viewModel)
        {
            Window window;

            if (viewModel is ConfirmationViewModel vm)
            {
                window = new ConfirmationView { DataContext = vm };
                vm.CloseRequested += result =>
                {
                    window.DialogResult = result;
                    window.Close();
                };
            }
            else
            {
                window = new Window { Content = viewModel };
            }

            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            return window.ShowDialog();
        }

    }
}
