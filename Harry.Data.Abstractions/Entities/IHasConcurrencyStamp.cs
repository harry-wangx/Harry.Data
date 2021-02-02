using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities
{
    public interface IHasConcurrencyStamp
    {
        string ConcurrencyStamp { get; set; }
    }
}
