using Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.MVVM.ViewModel
{
    class RegisterSellViewModel : Services.Navigation.ViewModel, IParameterReceiver
    {
        void IParameterReceiver.ReceiveParameter(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
