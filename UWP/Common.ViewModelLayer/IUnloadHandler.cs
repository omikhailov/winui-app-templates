using System;
using Common.DI.Attributes;

namespace Common.ViewModelLayer
{
    [NotForDI]

    public interface IUnloadHandler
    {
        void UnloadHandler();
    }
}
