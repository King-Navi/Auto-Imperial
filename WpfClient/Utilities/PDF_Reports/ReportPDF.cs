using AutoImperialDAO.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfClient.MVVM.View;
using WpfClient.Utilities.PDF_Reports.Models.ReportClient;
using WpfClient.Utilities.PDF_Reports.Models.ReportFinancial;
using WpfClient.Utilities.PDF_Reports.Models.ReportInventory;
using WpfClient.Utilities.PDF_Reports.Models.ReportSell;

namespace WpfClient.Utilities.PDF_Reports
{
    internal class ReportPDF : IReportPDF
    {
        private const string EXE_NAME = "pdfCreator.exe";
        private static readonly string JSON_CLIENT = "reporte_cliente.json";
        private static readonly string JSON_FINANCIAL = "reporte_financiero.json";
        private static readonly string JSON_INVENTORY = "reporte_inventario.json";
        private static readonly string JSON_SELL = "reporte_ventas.json";
        private static readonly string PATHS_JSONS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utilities", "PDF_Reports", "PythonPdf");
        
        private readonly string _outputFileName;
        private readonly int _idAdminEmployee;
        private readonly DateTime _startDateTime;
        private readonly string _adminEmployeeFullName;
        private readonly DateTime _endDateTime;
        private readonly PdfTypeEnum pdfType;
        private readonly string fullPdfPath;
        public ReportPDF(string outputPath, string outputFileName, int idAdminEmployee, DateTime startDateTime,
            DateTime endDateTime, string adminEmployeeFullName, PdfTypeEnum type)
        {
            _outputFileName = outputFileName.EndsWith(".pdf") ? outputFileName : outputFileName + ".pdf";
            _idAdminEmployee = idAdminEmployee;
            _startDateTime = startDateTime;
            _adminEmployeeFullName = adminEmployeeFullName;
            _endDateTime = endDateTime;
            pdfType = type;
            fullPdfPath = Path.Combine(outputPath, _outputFileName);
        }
        private string GetJsonFileName()
        {
            return pdfType switch
            {
                PdfTypeEnum.SellReportType => JSON_SELL,
                PdfTypeEnum.InventoryReportType => JSON_INVENTORY,
                PdfTypeEnum.FinancialReportType => JSON_FINANCIAL,
                PdfTypeEnum.ClientReportType => JSON_CLIENT,
                _ => throw new InvalidOperationException("Invalid PDF type.")
            };
        }
        private void WriteJsonToFile(object report, string fileName)
        {
            Directory.CreateDirectory(PATHS_JSONS);
            string path = Path.Combine(PATHS_JSONS, fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(report, options);
            File.WriteAllText(path, json);
        }

        public async Task<bool> GenerateSellAsync(List<Sale> saleParameter)
        {
            try
            {
                var report = new SalesReport
                {
                    Salesperson = _adminEmployeeFullName,
                    StartDate = _startDateTime,
                    EndDate = _endDateTime,
                    Sales = saleParameter
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                };

                WriteJsonToFile(report, JSON_SELL);
                return await PythonPdfGeneratorAsync();
            }
            catch (Exception)
            {

            }

            return false;
        }
        public async Task<bool> GenerateInventoryAsync(List<InventoryItem> inventoryParameter)
        {
            try
            {
                var report = new InventoryReport
                {
                    Salesperson = _adminEmployeeFullName,
                    StartDate = _startDateTime,
                    EndDate = _endDateTime,
                    Inventory = inventoryParameter
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                WriteJsonToFile(report, JSON_INVENTORY);
                return await PythonPdfGeneratorAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating inventory report: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GenerateFinancialAsync(List<FinancialSale> sales, List<FinancialPurchase> purchases)
        {
            try
            {
                decimal totalIncome = sales.Sum(s => s.Amount);
                decimal totalExpenses = purchases.Sum(p => p.Amount);

                var report = new FinancialReport
                {
                    Salesperson = _adminEmployeeFullName,
                    StartDate = _startDateTime,
                    EndDate = _endDateTime,
                    Income = totalIncome,
                    Expenses = totalExpenses,
                    Sales = sales,
                    Purchases = purchases
                };

                WriteJsonToFile(report, JSON_FINANCIAL);
                return await PythonPdfGeneratorAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating financial report: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GenerateClientAsync(string clientFullName, string rfc, string curp, List<ClientPurchase> purchases)
        {
            
            try
            {
                var report = new ClientReport
                {
                    Salesperson = _adminEmployeeFullName,
                    StartDate = _startDateTime,
                    EndDate = _endDateTime,
                    FullName = clientFullName,
                    RFC = rfc,
                    CURP = curp,
                    Purchases = purchases
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                WriteJsonToFile(report, JSON_CLIENT);
                return await PythonPdfGeneratorAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating client report: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> PythonPdfGeneratorAsync()
        {
            try
            {
                string exePath = Path.Combine(PATHS_JSONS, EXE_NAME);
                string jsonPath = Path.Combine(PATHS_JSONS, GetJsonFileName());
                string arguments = $"--plantilla {(int)pdfType} --json \"{jsonPath}\" --salida \"{fullPdfPath}\"";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"[PDF Generator Error] {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating PDF: {ex.Message}", ex);
            }
        }
    }
}
