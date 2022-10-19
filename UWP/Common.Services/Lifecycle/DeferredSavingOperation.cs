using System;
using Windows.System.Threading;
using CommunityToolkit.Mvvm.Messaging;
using Common.Services.Lifecycle.Messages;

namespace Common.Services.Lifecycle
{
    public class DeferredSavingOperation : IRecipient<SaveStateMessage>, IRecipient<AppEnteredBackgroundMessage>
    {
        private readonly Action _operation;

        private ThreadPoolTimer _timer;

        private TimeSpan _delay;

        public DeferredSavingOperation(Action operation, TimeSpan delay)
        {
            _operation = operation;

            _delay = delay;

            TryCreateTimer();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public bool IsCompleted
        {
            get
            {
                return _timer == null;
            }
        }

        public void Cancel()
        {
            _timer?.Cancel();

            _timer = null;
        }

        public void Schedule()
        {
            TryCreateTimer();
        }

        public void Receive(SaveStateMessage message)
        {
            Force();
        }

        public void Receive(AppEnteredBackgroundMessage message)
        {
            Force();
        }

        private void Force()
        {
            if (_timer != null) Cancel();

            _operation();
        }

        private void TryCreateTimer()
        {
            if (_timer == null)
            {
                _timer = ThreadPoolTimer.CreateTimer(timer => { _operation(); }, _delay, timer => _timer = null);
            }
        }
    }
}
