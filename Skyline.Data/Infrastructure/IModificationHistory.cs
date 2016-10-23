using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Infrastructure
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net.Configuration;

    public interface IModificationHistory
    {
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
        bool IsDirty { get; set; }
    }
}
