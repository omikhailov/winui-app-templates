using System.Threading.Tasks;

namespace Common.Services.Lifecycle
{
    public interface ILifecycleServiceBase
    {
        bool IsRunningInBackground { get; }

        Task Prelaunch();

        Task LoadState();

        Task SaveState();

        Task RestoreUnmanagedResources();

        Task DisposeUnmanagedResources();

        Task EnterBackground();

        Task LeaveBackground();
    }
}