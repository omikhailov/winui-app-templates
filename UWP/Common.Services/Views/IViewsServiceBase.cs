using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Common.Services.Views
{
    public interface IViewsServiceBase
    {
        IEnumerable<Page> ActivePages { get; }

        IEnumerable<Page> InactivePages { get; }

        IEnumerable<Page> AllPages { get; }

        void RegisterActivePage(Page page);

        void RegisterInactivePage(Page page);

        void UnregisterPage(Page page);
    }
}