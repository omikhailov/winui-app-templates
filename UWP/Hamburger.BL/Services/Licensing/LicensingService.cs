using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DI.Attributes;
using Common.Services.Licensing;

namespace Hamburger.BL.Services.Licensing
{
    [Singleton]

    public class LicensingService : LicensingServiceBase, ILicensingService, ILicensingServiceBase
    {
    }
}
