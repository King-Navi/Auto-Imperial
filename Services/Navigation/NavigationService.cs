using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> viewModelFacory;
        private ViewModel currentView;
        public ViewModel CurrentView
        {
            get => currentView;
            private set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }
        public NavigationService(Func<Type, ViewModel> _viewModelFacory)
        {
            viewModelFacory = _viewModelFacory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = viewModelFacory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : ViewModel
        {
            ViewModel viewModel = viewModelFacory.Invoke(typeof(TViewModel));

            if (viewModel is IParameterReceiver parameterReceiver)
            {
                parameterReceiver.ReceiveParameter(parameter);
            }

            CurrentView = viewModel;
        }
    }
}
