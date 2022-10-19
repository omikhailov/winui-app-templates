using System;
using System.Threading.Tasks;
using Windows.System;
using CommunityToolkit.Mvvm.Messaging;
using Common.DI;
using Common.Services.Lifecycle.Messages;
using Common.Services.Memory;

namespace Common.Services.Lifecycle
{
    public class LifecycleServiceBase : ILifecycleServiceBase
    {
        public LifecycleServiceBase()
        {
            MemoryManager.AppMemoryUsageLimitChanging += OnAppMemoryUsageLimitChanging;

            MemoryManager.AppMemoryUsageIncreased += OnAppMemoryUsageIncreased;
        }

        public bool IsRunningInBackground { get; private set; }

        public virtual async Task Prelaunch()
        {
            var response = WeakReferenceMessenger.Default.Send(new PrelaunchMessage());

            await response.CompletionTask;
        }

        public virtual async Task LoadState()
        {
            var response = WeakReferenceMessenger.Default.Send(new LoadStateMessage());

            await response.CompletionTask;
        }

        public virtual async Task SaveState()
        {
            var response = WeakReferenceMessenger.Default.Send(new SaveStateMessage());

            await response.CompletionTask;
        }

        public virtual async Task RestoreUnmanagedResources()
        {
            var response = WeakReferenceMessenger.Default.Send(new DisposeUnmanagedResourcesMessage());

            await response.CompletionTask;
        }

        public virtual async Task DisposeUnmanagedResources()
        {
            var response = WeakReferenceMessenger.Default.Send(new DisposeUnmanagedResourcesMessage());

            await response.CompletionTask;
        }

        public virtual async Task EnterBackground()
        {
            IsRunningInBackground = true;

            var response = WeakReferenceMessenger.Default.Send(new AppEnteredBackgroundMessage());

            await response.CompletionTask;
        }

        public virtual async Task LeaveBackground()
        {
            IsRunningInBackground = false;

            var response = WeakReferenceMessenger.Default.Send(new AppLeavingBackgroundMessage());

            await response.CompletionTask;
        }

        protected virtual async void OnAppMemoryUsageLimitChanging(object sender, AppMemoryUsageLimitChangingEventArgs e)
        {
            if (MemoryManager.AppMemoryUsage >= e.NewLimit)
            {
                MemoryCache.Clear();

                var response = WeakReferenceMessenger.Default.Send(new LowMemoryMessage(IsRunningInBackground));

                await response.CompletionTask;

                GC.Collect();
            }
        }

        protected virtual async void OnAppMemoryUsageIncreased(object sender, object e)
        {
            var level = MemoryManager.AppMemoryUsageLevel;

            if (level == AppMemoryUsageLevel.OverLimit || level == AppMemoryUsageLevel.High)
            {
                MemoryCache.Clear();

                var response = WeakReferenceMessenger.Default.Send(new LowMemoryMessage(IsRunningInBackground));

                await response.CompletionTask;

                GC.Collect();
            }
        }
    }
}
