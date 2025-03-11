using Microsoft.Extensions.DependencyInjection;
using Services.Navegation;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using WpfCliente.MVVM.View;
using WpfCliente.MVVM.Model;
using WpfCliente.MVVM.ViewModel;
using WpfCliente.Utilities;

namespace WpfCliente
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>( provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<LogInViewModel>();
            services.AddSingleton<SearchClientViewModel>();
            services.AddSingleton<RegisterClientViewModel>();
            services.AddSingleton<SideBarViewModel>();
            services.AddSingleton<UserService>();


            services.AddSingleton<INavegationService, NavegationService>();
            services.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            services.AddTransient<INavegationServiceFactory, NavegationServiceFactory>();

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
