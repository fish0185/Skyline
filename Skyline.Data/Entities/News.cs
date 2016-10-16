using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Entities
{
    using Skyline.Data.Infrastructure;

    public class News : IModificationHistory
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool IsDirty { get; set; }

        public int Id { get; set; }

        public string NewsTitle { get; set; }
    }
}
