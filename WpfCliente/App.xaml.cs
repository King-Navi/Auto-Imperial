using Microsoft.Extensions.DependencyInjection;
using Services.Navegation;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using WpfCliente.MVVM.View;
using WpfCliente.MVVM.ViewModel;
using WpfCliente.Utilities;

namespace WpfCliente
{
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>( provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<LogInViewModel>();
            services.AddSingleton<SearchClientViewModel>();
            services.AddSingleton<RegisterClientViewModel>();

            services.AddSingleton<INavegationService, NavegationService>();
            services.AddSingleton<Func<Type, ViewModel>>(provider =>
                viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

            services.AddTransient<INavegationServiceFactory, NavegationServiceFactory>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var  logInWindow = serviceProvider.GetRequiredService<MainWindow>();
            logInWindow.Show();
            base.OnStartup(e);
        }
    }
}
