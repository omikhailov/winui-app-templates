using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Unity;
using Common.DI;

namespace Hamburger
{
    partial class App
    {
        private void RegisterTypes()
        {
            if (Container.IsEmpty) Container.RegisterByConvention("Hamburger.BL");
        }

        private T Resolve<T>()
        {
            return Container.Instance.Resolve<T>();
        }
    }
}
