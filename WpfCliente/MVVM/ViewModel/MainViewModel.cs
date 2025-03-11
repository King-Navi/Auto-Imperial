using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfCliente.Utilities;

namespace WpfCliente.MVVM.ViewModel
{
    public class MainViewModel : Services.Navegation.ViewModel
    {
        private INavegationService navegation;

        private UserControl sideBar;

        public ICommand ShowSideBarCommand { get; set; }
        public ICommand HideSideBarCommand { get; set; }

        public INavegationService Navegation
        {
            get => navegation;
            set
            {
                navegation = value;
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

        public MainViewModel(INavegationService navegationService)
        {
            SideBar = null;
            Navegation = navegationService;
            Navegation.NavigateTo<LogInViewModel>();

            
            ShowSideBarCommand = new RelayCommand(ShowSideBar);
            HideSideBarCommand = new RelayCommand(HideSideBar);
            Mediator.Register("ShowSideBar", args => ShowSideBarCommand.Execute(null));
            Mediator.Register("HideSideBar", args => HideSideBarCommand.Execute(null));
        }

        private void ShowSideBar()
        {
            SideBar = new View.SideBarView();
        }

        private void HideSideBar()
        {
            SideBar = null;
        }





    }
}
