using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common.ViewModelLayer.Helpers;
using Common.ViewLayer.Windows;
using Hamburger.Views;
using Hamburger.BL.Services.Lifecycle;
using Hamburger.BL.Services.Telemetry;

namespace Hamburger
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.Suspending += OnSuspending;

            this.EnteredBackground += App_EnteredBackground;

            this.LeavingBackground += App_LeavingBackground;

            this.UnhandledException += App_UnhandledException;
        }

        private ILifecycleService _lifecycleService;

        private ITelemetryService _telemetryService;

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            RegisterTypes();

            _telemetryService = Resolve<ITelemetryService>();

            _lifecycleService = Resolve<ILifecycleService>();

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                TitleBarHelper.ExtendView();

                rootFrame = new Frame();

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated)
            {
                await _lifecycleService.Prelaunch();
            }
            else
            {
                if (e.PreviousExecutionState == ApplicationExecutionState.Suspended) await _lifecycleService.RestoreUnmanagedResources();

                if (e.PreviousExecutionState != ApplicationExecutionState.Running) await _lifecycleService.LoadState();

                CoreApplication.EnablePrelaunch(true);

                if (rootFrame.Content == null) rootFrame.Navigate(typeof(MainPage), e.Arguments);

                Window.Current.Activate();

                ApplicationView.GetForCurrentView().CloseSecondaryWindowsWhenConsolidated();
            }
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            await _lifecycleService.SaveState();

            await _lifecycleService.DisposeUnmanagedResources();

            deferral.Complete();
        }

        private async void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            await _lifecycleService.LeaveBackground();
        }

        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            await _lifecycleService.EnterBackground();
        }

        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            _telemetryService.Exception(e.Exception, $"Windows.UI.Xaml.UnhandledExceptionEventArgs.Handled = {e.Handled},  Windows.UI.Xaml.UnhandledExceptionEventArgs.Message = {e.Message}");
        }
    }
}
