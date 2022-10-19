using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DI.Attributes;
using Common.Services.Navigation;

namespace Hamburger.BL.Services.Navigation
{
    [Singleton(LazyLoading = false)]

    public class NavigationService : NavigationServiceBase
    {
    }
}
