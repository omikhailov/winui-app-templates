using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger.BL.Models.Entities
{
    public enum OrderStatus
    {
        Created,
        Accepted,
        Processed,
        Delivered,
        Cancelled
    }
}
