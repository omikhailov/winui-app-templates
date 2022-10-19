using System;
using Common.DI.Attributes;

namespace Common.ViewModelLayer
{
    [NotForDI]

    public interface INavigatedFromHandler
    {
        void NavigatedFromHandler(object parameter);
    }
}
