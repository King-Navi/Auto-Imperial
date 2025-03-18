﻿using Microsoft.Extensions.DependencyInjection;
using Services.Navigation;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
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
using System.Globalization;

namespace WpfClient
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            //Si alguien ve esto esta es la manera de inicializar la conexion a bd

            // Cargar la configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("AutoImperialDb");


            // inyectar AutoImperialContext con la cadena de conexión
            services.AddDbContext<AutoImperialContext>(options =>
                options.UseSqlServer(connectionString));

            // Registrar Repositorios
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ISellRepository, SellRepository>();
            services.AddTransient<IUserRepository, UserRepository>();


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
            services.AddSingleton<SearchSellViewModel>();
            services.AddSingleton<RegisterSellViewModel>();
            services.AddSingleton<ReserveViewModel>();

            services.AddSingleton<SideBarViewModel>();
            services.AddSingleton<AdminSideBarViewModel>();
            services.AddSingleton<UserService>();
            services.AddSingleton<SearchEmployeeViewModel>();
            services.AddSingleton<InfoEmployeeViewModel>();
            services.AddSingleton<RegisterEmployeeViewModel>();

            services.AddTransient<IDialogService, DialogService>();

            services.AddSingleton<INavigationService, Services.Navigation.NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //ChangeLanguage(IdiomsKeys.es_mx.ToString());
            var logInWindow = ServiceProvider.GetRequiredService<MainWindow>();
            logInWindow.Show();
            base.OnStartup(e);
        }
        public static void ChangeLanguage(string langCode)
        {
            const string RESOURCE_FOLDER = "Idioms";
            const string BASE_NAME = "strings";
            const string EXTENSION_FILE = "xalm";
            //string resourcePath = $"{RESOURCE_FOLDER}/{BASE_NAME}.{langCode}.{EXTENSION_FILE}";
            string resourcePath = $"Idioms/strings.{langCode}.xalm";

            var newDict = new ResourceDictionary
            {
                Source = new Uri(resourcePath, UriKind.Relative)
            };

            var existingDict = Application.Current.Resources.MergedDictionaries
                                 .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains($"{RESOURCE_FOLDER}."));

            if (existingDict != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(existingDict);
                Application.Current.Resources.MergedDictionaries[index] = newDict;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newDict);
            }
            CultureInfo culture = new CultureInfo(langCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

    }
}
