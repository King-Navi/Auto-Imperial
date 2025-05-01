using AutoImperialDAO.DAO.Interfaces;
using System.Diagnostics;
using System.Windows.Input;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.View;
using WpfClient.Utilities;
using WpfClient.Utilities.PDF_Reports;
using WpfClient.Utilities.PDF_Reports.Models.ReportSell;

namespace WpfClient.MVVM.ViewModel
{
    public class ReportsViewModel : Services.Navigation.ViewModel
    {
        private readonly UserService employee;
        private bool CanGenerateReports =>
            StartDate != null &&
            EndDate != null &&
            !string.IsNullOrWhiteSpace(NameFileOutput) &&
            !string.IsNullOrWhiteSpace(SelectedPath);
        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; OnPropertyChanged(); RaiseCommandsCanExecuteChanged(); }
        }
        private string _nameFileOutput;
        public string NameFileOutput
        {
            get => _nameFileOutput;
            set { _nameFileOutput = value; OnPropertyChanged(); RaiseCommandsCanExecuteChanged(); }
        }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                RaiseCommandsCanExecuteChanged();
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
                RaiseCommandsCanExecuteChanged();
            }
        }
        private RelayCommand _generateSellsReportCommand;
        private RelayCommand _generateInventoryReportCommand;
        private RelayCommand _generateFinancialReportCommand;
        private RelayCommand _generateClientReportCommand;
        public ICommand GenerateSellsReportCommand => _generateSellsReportCommand;
        public ICommand GenerateInventoryReportCommand => _generateInventoryReportCommand;
        public ICommand GenerateFinancialReportCommand => _generateFinancialReportCommand;
        public ICommand GenerateClientReportCommand => _generateClientReportCommand;
        public IAdministrator RepoAdmin { get; set; }
        public ISellRepository RepoSell { get; set; }
        public IVehicleRepository RepoVehicle { get; set; }
        public ISupplierPaymentRepository RepoSupplierPaymentRepository { get; set; }
        public IClientRepository RepoClient { get; set; }
        public string CLientCURP { get; set; }
        public ReportsViewModel(UserService user, IAdministrator repoAdmin, ISellRepository sellRepository, IVehicleRepository vehicleRepository, 
            ISupplierPaymentRepository repoSupplierPaymentRepository, IClientRepository repoClient)
        {
            employee = user;
            _generateSellsReportCommand = new RelayCommand(GenerateSellsReportAsync, o => CanGenerateReports);
            _generateInventoryReportCommand = new RelayCommand(GenerateInventoryReportAsync, o => CanGenerateReports);
            _generateFinancialReportCommand = new RelayCommand(GenerateFinancialReportAsync, o => CanGenerateReports);
            _generateClientReportCommand = new RelayCommand(GenerateClientReportAsync, o => CanGenerateReports);
            RepoAdmin = repoAdmin;
            RepoSell = sellRepository;
            RepoVehicle = vehicleRepository;
            RepoSupplierPaymentRepository = repoSupplierPaymentRepository;
            RepoClient = repoClient;
        }
        private void RaiseCommandsCanExecuteChanged()
        {
            _generateSellsReportCommand.RaiseCanExecuteChanged();
            _generateInventoryReportCommand.RaiseCanExecuteChanged();
            _generateFinancialReportCommand.RaiseCanExecuteChanged();
            _generateClientReportCommand.RaiseCanExecuteChanged();
        }
        private async void GenerateSellsReportAsync(object obj)
        {
            var adminEmployee = await RepoAdmin.SearchByIdAsync(employee.CurrentUser.Id, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
            var fullName = $"{adminEmployee.nombre} {adminEmployee.apellidoPaterno} {adminEmployee.apellidoMaterno}";
            var saleDataList = RepoSell.GetSalesReport(StartDate.Value, EndDate.Value);

            var sales = saleDataList.Select(item => new Sale
            {
                Salesperson = item.Salesperson,
                Customer = item.Customer,
                Vehicle = item.Vehicle,
                Type = item.Type,
                Amount = item.Amount ?? 0
            }).ToList();

            ReportPDF reportPDF = new ReportPDF(
                SelectedPath,
                NameFileOutput,
                employee.CurrentUser.Id,
                StartDate.Value,
                EndDate.Value,
                fullName,
                PdfTypeEnum.SellReportType
            );

            await reportPDF.GenerateSellAsync(sales);
        }

        private async void GenerateInventoryReportAsync(object obj)
        {
            var adminEmployee = await RepoAdmin.SearchByIdAsync(employee.CurrentUser.Id, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
            var fullName = $"{adminEmployee.nombre} {adminEmployee.apellidoPaterno} {adminEmployee.apellidoMaterno}";
            var inventoryDataList = RepoVehicle.GetCurrentInventory(StartDate.Value, EndDate.Value);
            var inventory = inventoryDataList.Select(item => new WpfClient.Utilities.PDF_Reports.Models.ReportInventory.InventoryItem
            {
                Model = item.Model,
                Version = item.Version,
                Brand = item.Brand
            }).ToList();
            var reportPDF = new ReportPDF(
                SelectedPath,
                NameFileOutput,
                employee.CurrentUser.Id,
                StartDate.Value,
                EndDate.Value,
                fullName,
                PdfTypeEnum.InventoryReportType
            );
            await reportPDF.GenerateInventoryAsync(inventory);
        }
        
        private async void GenerateFinancialReportAsync(object obj)
        {
            try
            {
                var adminEmployee = await RepoAdmin.SearchByIdAsync(
                    employee.CurrentUser.Id,
                    AutoImperialDAO.Enums.AccountStatusEnum.Activo
                );
                var fullName = $"{adminEmployee.nombre} {adminEmployee.apellidoPaterno} {adminEmployee.apellidoMaterno}";
                var salesDto = RepoSell.GetFinancialSales(StartDate.Value, EndDate.Value);
                var purchasesDto = RepoSupplierPaymentRepository.GetFinancialPurchases(StartDate.Value, EndDate.Value);
                var sales = salesDto.Select(s => new WpfClient.Utilities.PDF_Reports.Models.ReportFinancial.FinancialSale
                {
                    Model = s.Model,
                    Version = s.Version,
                    Brand = s.Brand,
                    Amount = s.Amount
                }).ToList();
                var purchases = purchasesDto.Select(p => new WpfClient.Utilities.PDF_Reports.Models.ReportFinancial.FinancialPurchase
                {
                    Model = p.Model,
                    Version = p.Version,
                    Brand = p.Brand,
                    Supplier = p.Supplier,
                    Amount = p.Amount
                }).ToList();
                var reportPDF = new ReportPDF(
                    SelectedPath,
                    NameFileOutput,
                    employee.CurrentUser.Id,
                    StartDate.Value,
                    EndDate.Value,
                    fullName,
                    PdfTypeEnum.FinancialReportType
                );

                await reportPDF.GenerateFinancialAsync(sales, purchases);
            }
            catch (Exception)
            {
            }
        }
        private async void GenerateClientReportAsync(object obj)
        {
            var inputDialog = new InputDialog("Introduzca el CURP del cliente:");
            if (inputDialog.ShowDialog() != true)
            {
                return;
            }

            string value = inputDialog.ResponseText;
            CLientCURP = value;

            if (!CanGenerateReports)
            {
                System.Windows.MessageBox.Show("Por favor completa todos los campos antes de generar el reporte.");
                return;
            }
            try
            {
                var adminEmployee = await RepoAdmin.SearchByIdAsync(
                    employee.CurrentUser.Id,
                    AutoImperialDAO.Enums.AccountStatusEnum.Activo
                );
                var fullNameAdmin = $"{adminEmployee.nombre} {adminEmployee.apellidoPaterno} {adminEmployee.apellidoMaterno}";
                var client = await RepoClient.SearchByCURPAsync(CLientCURP, AutoImperialDAO.Enums.AccountStatusEnum.Activo);
                var clientId = client.idCliente;
                var clientFullName = $"{client.nombre} {client.apellidoPaterno} {client.apellidoMaterno}";
                var clientRFC = client.RFC;
                var clientCURP = client.CURP;
                var purchaseDtoList = RepoClient.GetClientPurchases(clientId, StartDate.Value, EndDate.Value);
                var purchases = purchaseDtoList.Select(p => new WpfClient.Utilities.PDF_Reports.Models.ReportClient.ClientPurchase
                {
                    Model = p.Model,
                    Version = p.Version,
                    Brand = p.Brand
                }).ToList();
                var reportPDF = new ReportPDF(
                    SelectedPath,
                    NameFileOutput,
                    employee.CurrentUser.Id,
                    StartDate.Value,
                    EndDate.Value,
                    fullNameAdmin,
                    PdfTypeEnum.ClientReportType
                );

                await reportPDF.GenerateClientAsync(clientFullName, clientRFC, clientCURP, purchases);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating client report: {ex.Message}");
            }
        }
        

    }
}
