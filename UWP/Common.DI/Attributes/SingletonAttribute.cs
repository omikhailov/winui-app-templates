using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DI.Attributes
{
    public class SingletonAttribute : Attribute
    {
        public bool LazyLoading { get; set; }
    }
}
