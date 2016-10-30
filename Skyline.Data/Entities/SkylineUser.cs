using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skyline.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity.EntityFramework;
    public class SkylineUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DOB { get; set; }
    }
}