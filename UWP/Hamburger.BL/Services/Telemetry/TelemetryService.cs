using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DI.Attributes;
using Common.Services.Telemetry;
using Common.Services.Telemetry.AppCenter;

namespace Hamburger.BL.Services.Telemetry
{
    [Singleton(LazyLoading = false)]

    public class TelemetryService : TelemetryServiceBase, ITelemetryService, ITelemetryServiceBase
    {
        public TelemetryService()
        {
            base.Start("f123f1b2-c144-41f3-8eb9-745a730a0b66");
        }
    }
}
