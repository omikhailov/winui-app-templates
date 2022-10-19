using System;
using Common.DI.Attributes;

namespace Common.ViewModelLayer
{
    [NotForDI]

    public interface INavigatingFromHandler
    {
        bool NavigatingFromHandler(object parameter);
    }
}
