using System;
using Common.DI.Attributes;
using Common.Services.Views;

namespace Hamburger.BL.Services.Views
{
    [Singleton]

    public class ViewsService : ViewsServiceBase, IViewsService, IViewsServiceBase
    {

    }
}
