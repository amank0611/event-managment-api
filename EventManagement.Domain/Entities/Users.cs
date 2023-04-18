using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
