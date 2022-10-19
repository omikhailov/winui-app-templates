using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Common.DI.Attributes;
using Common.Services.Lifecycle;

namespace Hamburger.BL.Services.Lifecycle
{
    [Singleton(LazyLoading = false)]

    public class LifecycleService : LifecycleServiceBase, ILifecycleService
    {
    }
}
