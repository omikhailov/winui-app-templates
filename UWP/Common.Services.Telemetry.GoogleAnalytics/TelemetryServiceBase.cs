using System;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.Helpers;
using GoogleAnalytics;

namespace Common.Services.Telemetry.AppCenter
{
    public class TelemetryServiceBase : ITelemetryServiceBase
    {
        protected virtual void Start(string key)
        {
            var config = new EasyTrackerConfig();

            config.TrackingId = key;

            config.AppName = SystemInformation.Instance.ApplicationName;

            config.AppVersion = SystemInformation.Instance.ApplicationVersion.ToFormattedString();

            EasyTracker.Current.Config = config;
        }

        public virtual void PageHit(string pageName)
        {
            try 
            { 
                EasyTracker.GetTracker().SendView(pageName); 
            } 
            catch { }
        }

        public virtual void Event(string category, string action)
        {
            try 
            { 
                EasyTracker.GetTracker().SendEvent(category, action, null, 0); 
            } 
            catch { }
        }

        public virtual void Exception(Exception exception, string details = null, 
            [CallerMemberName] string location = null, 
            [CallerFilePath] string sourceFile = null, 
            [CallerLineNumber] int sourceLine = 0)
        {
            void Send()
            {
                var isUnhandled = !string.IsNullOrEmpty(location) && location.Contains("Unhandled", StringComparison.InvariantCultureIgnoreCase);

                var description = new StringBuilder();

                if (!string.IsNullOrEmpty(location))
                {
                    description.Append("Location: ");

                    description.AppendLine(location);
                }

                if (!string.IsNullOrEmpty(sourceFile))
                {
                    description.Append("Source File: ");

                    description.AppendLine(sourceFile);
                }

                if (sourceLine > 0)
                {
                    description.Append("Source Line: ");

                    description.AppendLine(sourceLine.ToString());
                }

                if (!string.IsNullOrEmpty(details))
                {
                    description.Append("Details: ");

                    description.AppendLine(details);
                }

                description.AppendLine("Exception: ");

                description.Append(exception.ToString());

                EasyTracker.GetTracker().SendException(description.ToString(), isUnhandled);
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
