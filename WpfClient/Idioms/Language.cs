using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient.Idioms
{
    //It should be on the services layer
    public static class Language
    {   

        public static string GetLocalizedString(TextKeys key)
        {
            return Application.Current.Resources[key.ToString()] as string ?? key.ToString();
        }

        
    }
}
