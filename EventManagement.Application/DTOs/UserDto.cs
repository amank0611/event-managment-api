using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int RoleId { get; set; }
        //public string Token { get; set; }
        //public string RefreshToken { get; set; }
        //public int LoginType { get; set; }
    }
    public class AuthenticationDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

    }
}
