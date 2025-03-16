using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    public class MainViewModel : Services.Navigation.ViewModel
    {
        private UserControl sideBar;

        public ICommand ShowSideBarCommand { get; set; }
        public ICommand HideSideBarCommand { get; set; }
        public ICommand ShowAdminSideBarCommand { get; set; }

        private INavigationService navigation;
        public INavigationService Navigation
        {
            get => navigation;
            set
            {
                navigation = value;
                OnPropertyChanged();
            }
        }

        public UserControl SideBar
        {
            get => sideBar;
            set
            {
                sideBar = value;
                OnPropertyChanged(nameof(SideBar));
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            SideBar = null;
            Navigation = navigationService;
            Navigation.NavigateTo<LogInViewModel>();
            InicializateCommands();
            RegisterMediator();
        }

        private void InicializateCommands()
        {
            ShowSideBarCommand = new RelayCommand(ShowSideBar);
            HideSideBarCommand = new RelayCommand(HideSideBar);
            ShowAdminSideBarCommand = new RelayCommand(ShowAdminSideBar);
        }

        private void RegisterMediator()
        {
            Mediator.Register(MediatorKeys.SHOW_SIDE_BAR, args => ShowSideBarCommand.Execute(null));
            Mediator.Register(MediatorKeys.HIDE_SIDE_BAR, args => HideSideBarCommand.Execute(null));
            Mediator.Register(MediatorKeys.SHOW_ADMIN_SIDE_BAR, args => ShowAdminSideBarCommand.Execute(null));
        }

        private void ShowSideBar()
        {
            SideBar = new View.SideBarView();
        }

        private void ShowAdminSideBar()
        {
            SideBar = new View.AdminSideBarView();
        }

        private void HideSideBar()
        {
            SideBar = null;
        }





    }
}
