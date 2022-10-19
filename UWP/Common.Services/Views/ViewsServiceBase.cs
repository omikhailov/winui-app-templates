using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Common.Services.Views
{
    public class ViewsServiceBase : IViewsServiceBase
    {
        protected List<Page> _activePages = new List<Page>();

        public IEnumerable<Page> ActivePages
        {
            get
            {
                return _activePages;
            }
        }

        protected List<Page> _inactivePages = new List<Page>();

        public IEnumerable<Page> InactivePages
        {
            get
            {
                return _activePages;
            }
        }

        public IEnumerable<Page> AllPages
        {
            get
            {
                return _activePages.Concat(_inactivePages);
            }
        }

        public virtual void RegisterActivePage(Page page)
        {
            _activePages.Add(page);
        }

        public virtual void RegisterInactivePage(Page page)
        {
            _activePages.Remove(page);

            _inactivePages.Add(page);
        }

        public virtual void UnregisterPage(Page page)
        {
            _activePages.Remove(page);

            _inactivePages.Remove(page);
        }
    }
}
