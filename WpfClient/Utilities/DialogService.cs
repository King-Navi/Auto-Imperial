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
    public class DialogService : IDialogService
    {
        private readonly Dictionary<Type, Type> _viewModelToViewMapping = new();
        public DialogService()
        {
            Register<ConfirmationViewModel, ConfirmationView>();
            Register<AlertViewModel, AlertView>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class
            where TView : Window, new()
        {
            _viewModelToViewMapping[typeof(TViewModel)] = typeof(TView);
        }

        public bool? ShowDialog(object viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (!_viewModelToViewMapping.TryGetValue(viewModel.GetType(), out Type viewType))
                throw new InvalidOperationException($"No se encontró una vista registrada para {viewModel.GetType().Name}");

            Window window = (Window)Activator.CreateInstance(viewType)!;
            window.DataContext = viewModel;

            if (viewModel is ICloseable closeableViewModel)
            {
                closeableViewModel.CloseRequested += result =>
                {
                    window.DialogResult = result;
                    window.Close();
                };
            }

            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            return window.ShowDialog();
        }
    }
}
