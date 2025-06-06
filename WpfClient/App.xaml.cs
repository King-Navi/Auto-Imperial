﻿using Microsoft.Extensions.DependencyInjection;
using Services.Navigation;
using System.Windows;
using WpfClient.MVVM.View;
using WpfClient.MVVM.Model;
using WpfClient.MVVM.ViewModel;
using WpfClient.Utilities;
using Services.Dialogs;
using AutoImperialDAO.DAO.Interfaces;
using AutoImperialDAO.DAO.Repositories;
using AutoImperialDAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WpfClient.Idioms;

namespace WpfClient
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString("AutoImperialDb");
            services.AddDbContext<AutoImperialContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ISellRepository, SellRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISupplierPaymentRepository, SupplierPaymentRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IReserveRepository, ReserveRepository>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();
            services.AddTransient<IAdministrator, AdministratorRepository>();

            //Navigation
            services.AddSingleton<MainWindow>( provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LogInViewModel>();

            services.AddSingleton<SearchClientViewModel>();
            services.AddSingleton<RegisterClientViewModel>();

            services.AddTransient<SearchSellViewModel>();
            services.AddTransient<RegisterSellViewModel>();
            services.AddSingleton<ReserveViewModel>();
            services.AddTransient<InfoSellViewModel>();
            services.AddTransient<EditSellViewModel>();

            services.AddTransient<SideBarViewModel>();
            services.AddTransient<AdminSideBarViewModel>();
            services.AddSingleton<UserService>();
            services.AddTransient<SearchEmployeeViewModel>();
            services.AddTransient<InfoEmployeeViewModel>();
            services.AddTransient<RegisterEmployeeViewModel>();
            services.AddTransient<EditEmployeeViewModel>();

            services.AddTransient<RegisterSupplierViewModel>();
            services.AddTransient<SearchSupplierViewModel>();
            services.AddTransient<InfoSupplierViewModel>();

            services.AddTransient<RegisterSupplierPaymentViewModel>();
            services.AddTransient<SearchSupplierPaymentViewModel>();
            services.AddSingleton<InfoSupplierPaymentViewModel>();

            services.AddSingleton<SearchVehicleViewModel>();
            services.AddTransient<InfoVehicleViewModel>();
            services.AddTransient<EditVehicleViewModel>();
            services.AddTransient<ReportsViewModel>();


            services.AddTransient<IDialogService, DialogService>();

            services.AddSingleton<INavigationService, Services.Navigation.NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Language.ChangeLanguage(IdiomsKeys.es_mx.ToString());
            var logInWindow = ServiceProvider.GetRequiredService<MainWindow>();
            logInWindow.Show();
            base.OnStartup(e);
        }
        

    }
}
