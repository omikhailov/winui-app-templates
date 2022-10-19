using System;
using Common.DI.Attributes;

namespace Common.ViewModelLayer
{
    [NotForDI]

    public interface INavigatedToHandler
    {
        void NavigatedToHandler(object parameter);
    }
}
