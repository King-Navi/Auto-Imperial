using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.Models;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using System.Text.RegularExpressions;
using WpfClient.Utilities.Enum;

namespace WpfClient.MVVM.ViewModel
{
    class RegisterSupplierViewModel : Services.Navigation.ViewModel
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

        public RegisterSupplierViewModel(INavigationService navigationService, UserService currentUser, ISupplierRepository supplierRepository, IDialogService dialogService)
        {
            _dialogService = dialogService;
            Navigation = navigationService;
            _supplierRepository = supplierRepository;
            NavigateToSearchSupplierView = new RelayCommand(NavigateToSearchSupplier);
            RegisterSupplierCommand = new RelayCommand(RegisterSupplier);

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
        private void RegisterSupplier()
        {
            if (!ValidateFields())
                return;

            var confirmationVM = new ConfirmationViewModel("Confirmación de registro",
                "¿Está seguro que desea registrar a este nuevo proveedor?",
                ConfirmationIconType.RegisterIcon);
            var result = _dialogService.ShowDialog(confirmationVM);
            if (result == true)
            {
                Proveedor supplier = new Proveedor
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
                    if (_supplierRepository.Register(supplier))
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

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(SupplierName) ||
                string.IsNullOrWhiteSpace(Street) ||
                string.IsNullOrWhiteSpace(Number) ||
                string.IsNullOrWhiteSpace(ZipCode) ||
                string.IsNullOrWhiteSpace(City) ||
                string.IsNullOrWhiteSpace(Phone) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PrimaryContact))
            {
                _dialogService.ShowDialog(new AlertViewModel("Campos vacíos",
                    "Todos los campos son obligatorios.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!int.TryParse(Number, out _))
            {
                _dialogService.ShowDialog(new AlertViewModel("Número inválido",
                    "El número debe ser un valor entero.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(ZipCode, @"^\d{5}$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Código Postal inválido",
                    "El código postal debe contener 5 dígitos.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Phone, @"^\d{10}$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Teléfono inválido",
                    "El número de teléfono debe contener 10 dígitos.",
                    AlertIconType.AlertIcon));
                return false;
            }

            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _dialogService.ShowDialog(new AlertViewModel("Correo inválido",
                    "El correo electrónico no es válido.",
                    AlertIconType.AlertIcon));
                return false;
            }

            return true;
        }
    }
}