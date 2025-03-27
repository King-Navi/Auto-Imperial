using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class RegisterSupplierPaymentViewModel : Services.Navigation.ViewModel
    {
        private string supplierName;
        public string SupplierName
        {
            get => supplierName;
            set { supplierName = value; OnPropertyChanged(); }
        }

        private string street;
        public string Street
        {
            get => street;
            set { street = value; OnPropertyChanged(); }
        }

        private string number;
        public string Number
        {
            get => number;
            set { number = value; OnPropertyChanged(); }
        }

        private string zipCode;
        public string ZipCode
        {
            get => zipCode;
            set { zipCode = value; OnPropertyChanged(); }
        }

        private string city;
        public string City
        {
            get => city;
            set { city = value; OnPropertyChanged(); }
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }

        private string email;
        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        private string primaryContact;
        public string PrimaryContact
        {
            get => primaryContact;
            set { primaryContact = value; OnPropertyChanged(); }
        }


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

        public ICommand NavigateToSearchSupplierView { get; set; }
        public ICommand RegisterSupplierCommand { get; set; }

        private readonly ISupplierRepository _supplierRepository;
        private readonly IDialogService _dialogService;

        public RegisterSupplierPaymentViewModel(INavigationService navigationService, UserService currentUser, ISupplierRepository supplierRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _supplierRepository = supplierRepository;
            NavigateToSearchSupplierView = new RelayCommand(NavigateToSearchSupplier);
            RegisterSupplierCommand = new RelayCommand(RegisterEmployee);

        }

        private void NavigateToSearchSupplier()
        {
            var confirmationVM = new ConfirmationViewModel("Cancerlar registro", $"¿Esta seguro que desea salir del registro? Se perderán todos los cambios no guardados", Utilities.Enum.ConfirmationIconType.WarningIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Navigation.NavigateTo<SearchSupplierViewModel>();
            }
        }
        private void RegisterEmployee()
        {
            var confirmationVM = new ConfirmationViewModel("Confimracion de registro", $"¿Esta seguro que desea registrar a este nuevo empleado?", Utilities.Enum.ConfirmationIconType.RegisterIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Proveedor employee = new Proveedor
                {
                    nombreProveedor = this.SupplierName,
                    calle = this.Street,
                    numero = int.TryParse(this.Number, out int parsedNumber) ? parsedNumber : 0,
                    codigoPostal = this.ZipCode,
                    ciudad = this.City,
                    telefono = this.Phone,
                    correo = this.Email,
                    contactoPrincipal = this.PrimaryContact
                };

                try
                {

                    if (_supplierRepository.Register(employee))
                    {
                        MessageBox.Show("Proveedor registrado correctamente");
                        Navigation.NavigateTo<SearchSupplierViewModel>();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar el proveedor");

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al registrar el proveedor: " + e.StackTrace);
                }
            }
        }
    }
}
