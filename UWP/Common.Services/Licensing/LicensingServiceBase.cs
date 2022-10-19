using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;

namespace Common.Services.Licensing
{
    public class LicensingServiceBase : ILicensingServiceBase
    {
        public string AppName
        {
            get
            {
                return SystemInformation.Instance.ApplicationName;
            }
        }

        public string AppVersion
        {
            get
            {
                var version = SystemInformation.Instance.ApplicationVersion;

                return version.ToFormattedString();
            }
        }

        public async Task ReviewApp()
        {
            await SystemInformation.LaunchStoreForReviewAsync();
        }
    }
}
