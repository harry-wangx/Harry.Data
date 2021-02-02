using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Data.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
