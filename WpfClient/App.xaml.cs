using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<ReserveViewModel>();
            services.AddSingleton<SideBarViewModel>();
            services.AddSingleton<AdminSideBarViewModel>();
            services.AddSingleton<UserService>();
            services.AddSingleton<SearchEmployeeViewModel>();

            services.AddTransient<IDialogService, DialogService>();

            services.AddSingleton<INavigationService, Services.Navigation.NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var  logInWindow = ServiceProvider.GetRequiredService<MainWindow>();
            logInWindow.Show();
            base.OnStartup(e);
        }
    }
}
