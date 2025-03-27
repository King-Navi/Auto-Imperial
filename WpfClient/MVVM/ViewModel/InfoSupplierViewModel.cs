using AutoImperialDAO.DAO.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Dialogs;
using Services.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;
using System.Windows;
using WpfClient.MVVM.Model;
using WpfClient.Utilities;
using AutoImperialDAO.DAO.Repositories;

namespace WpfClient.MVVM.ViewModel
{
    class InfoSupplierViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        private Supplier _actualSupplier = new Supplier();
        private Supplier? _originalSupplier;

        public string? SupplierName { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? ZipCode { get; set; }
        public string?   City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PrimaryContact { get; set; }

        public Supplier ActualSupplier
        {
            get => _actualSupplier;
            set
            {
                _actualSupplier = value;
                OnPropertyChanged();
            }
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
        private readonly IDialogService _dialogService;
        private readonly ISupplierRepository _supplierRepository;


        public ICommand NavigateToSearchSupplierView { get; set; }
        public ICommand NavigateToRegisterSupplierPaymentCommand { get; set; }
        public ICommand NavigateToSearchSupplierPaymentCommand { get; set; }
        public InfoSupplierViewModel(INavigationService navigationService, IDialogService dialogService, ISupplierRepository supplierRepository)
        {
            _dialogService = dialogService;
            _supplierRepository = supplierRepository;
            Navigation = navigationService;

            NavigateToSearchSupplierView = new RelayCommand(NavigateToSearchSupplier);
            NavigateToRegisterSupplierPaymentCommand = new RelayCommand(RegisterSupplierPayment);
            NavigateToSearchSupplierPaymentCommand = new RelayCommand(SearchSupplierPayment);
        }
        public void ReceiveParameter(object parameter)
        {
            if (parameter is Supplier supplier)
            {
                ActualSupplier = supplier;
                _originalSupplier = (Supplier)supplier.Clone();

                InitProperties();
            }
            else
            {
                MessageBox.Show("Error al cargar el proveedor");
            }
        }

        private void NavigateToSearchSupplier()
        {
            Navigation.NavigateTo<SearchSupplierViewModel>();
        }

        private void RegisterSupplierPayment()
        {
            Navigation.NavigateTo<RegisterSupplierPaymentViewModel>(ActualSupplier);
        }

        void SearchSupplierPayment()
        {
            Navigation.NavigateTo<SearchSupplierPaymentViewModel>(ActualSupplier);
        }


        private void InitProperties()
        {
            SupplierName = ActualSupplier.SupplierName;
            Street = ActualSupplier.Street;
            Number = ActualSupplier.Number;
            ZipCode = ActualSupplier.ZipCode;
            City = ActualSupplier.City;
            Phone = ActualSupplier.Phone;
            Email = ActualSupplier.Email;
            PrimaryContact = ActualSupplier.PrimaryContact;
        }
    }
}
