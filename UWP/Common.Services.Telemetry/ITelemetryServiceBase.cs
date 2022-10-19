using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.Services.Telemetry
{
    public interface ITelemetryServiceBase
    {
        void Event(string category, string action);

        void Exception(Exception exception, string details = null, 
            [CallerMemberName] string location = null, 
            [CallerFilePath] string sourceFile = null, 
            [CallerLineNumber] int sourceLine = 0);

        void PageHit(string pageName);
    }
}