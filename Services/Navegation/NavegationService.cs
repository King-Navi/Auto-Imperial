using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Navegation
{
    public class NavegationService : ObservableObject, INavegationService
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
        public NavegationService(Func<Type, ViewModel> _viewModelFacory)
        {
            viewModelFacory = _viewModelFacory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = viewModelFacory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
