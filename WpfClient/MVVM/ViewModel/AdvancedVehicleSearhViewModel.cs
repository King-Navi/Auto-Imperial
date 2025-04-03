using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfClient.Utilities;

namespace WpfClient.MVVM.ViewModel
{
    class AdvancedVehicleSearhViewModel : Services.Navigation.ViewModel
    {
        private string? color;
        public string? Color
        {
            get => color;
            set { color = value; OnPropertyChanged(); }
        }

        private string? version;
        public string? Version
        {
            get => version;
            set { version = value; OnPropertyChanged(); }
        }

        private string? year;
        public string? Year
        {
            get => year;
            set { year = value; OnPropertyChanged(); }
        }

        private string? priceMin;
        public string? PriceMin
        {
            get => priceMin;
            set { priceMin = value; OnPropertyChanged(); }
        }

        private string? priceMax;
        public string? PriceMax
        {
            get => priceMax;
            set { priceMax = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }

        public AdvancedVehicleSearhViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
        }

        private void ExecuteSearch(object parameter)
        {
            try
            {
                int parsedMaxPrice = int.TryParse(priceMax, out var max) ? max : 0;
                int? parsedMinPrice = int.TryParse(priceMin, out var min) ? min : null;

                var vehicleSearch = new AutoImperialDAO.Utilities.VehicleSearch(
                    color: Color,
                    version: Version,
                    year: Year,
                    maxPrice: parsedMaxPrice,
                    minPrice: parsedMinPrice ?? 0
                );

               Mediator.Notify(MediatorKeys.ADVANCED_VEHICLE_SEARCH, vehicleSearch);
                Color = string.Empty;
                Version = string.Empty;
                Year = string.Empty;
                PriceMin = string.Empty;
                PriceMax = string.Empty;

                if (parameter is Window window)
                {
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}");
            }
        }

        
    }
}