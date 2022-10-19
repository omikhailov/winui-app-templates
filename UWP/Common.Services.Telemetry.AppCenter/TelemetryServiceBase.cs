using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Tracker = Microsoft.AppCenter.AppCenter;

namespace Common.Services.Telemetry.AppCenter
{
    public class TelemetryServiceBase : ITelemetryServiceBase
    {
        protected virtual void Start(string key)
        {
            try
            {
                var countryCode = GlobalizationPreferences.HomeGeographicRegion;

                if (string.IsNullOrEmpty(countryCode)) countryCode = RegionInfo.CurrentRegion?.TwoLetterISORegionName;

                if (!string.IsNullOrEmpty(countryCode)) Tracker.SetCountryCode(countryCode);
            }
            catch { }

            Tracker.Start(key, typeof(Analytics), typeof(Crashes));
        }

        public virtual void PageHit(string pageName)
        {
            Event("Page Hit", pageName);
        }

        public virtual void Event(string category, string action)
        {
            try
            {
                Analytics.TrackEvent(action, new Dictionary<string, string> { { "Category", category } });
            }
            catch { }
        }

        public virtual void Exception(Exception exception, string details = null, 
            [CallerMemberName] string location = null, 
            [CallerFilePath] string sourceFile = null, 
            [CallerLineNumber] int sourceLine = 0
            )
        {
            void Send()
            {
                var data = new Dictionary<string, string>() 
                { 
                    { "Details", details }, 
                    { "Location", location }, 
                    { "Source File", sourceFile }, 
                    { "Source Line",  sourceLine.ToString()} 
                };

                Crashes.TrackError(exception, data);
            }

#if DEBUG
            if (location != null) System.Diagnostics.Debug.WriteLine("Exception at " + location);

            System.Diagnostics.Debug.WriteLine(exception.ToString());
#endif
            if (exception is OutOfMemoryException)
            {
                try
                {
                    GC.Collect();

                    Send();
                }
                catch { }
                finally
                {
                    Application.Current.Exit();
                }
            }
            else
            {
                Send();
            }
        }
    }
}
