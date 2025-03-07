using Microsoft.Extensions.DependencyInjection;
using Services.Navegation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfCliente.Utilities.NavegationServiceFactory;

namespace WpfCliente.Utilities
{
    internal class NavegationServiceFactory : INavegationServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NavegationServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INavegationService CreateNavigationService()
        {
            var viewModelFactory = _serviceProvider.GetRequiredService<Func<Type, ViewModel>>();
            return new NavegationService(viewModelFactory);
        }
    }
}
