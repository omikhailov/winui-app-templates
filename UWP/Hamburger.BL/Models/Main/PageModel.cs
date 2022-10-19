using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hamburger.BL.Models.Main
{
    public class PageModel
    {
        public string Name { get; set; }

        public Symbol Icon { get; set; }

        public override string ToString()
        {
            return Name + " PageModel";
        }

        public object Parameter { get; set; }
    }
}
