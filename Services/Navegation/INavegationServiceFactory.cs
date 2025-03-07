﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Navegation
{
    public interface INavegationServiceFactory
    {
        INavegationService CreateNavigationService();
    }
}
