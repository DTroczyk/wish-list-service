using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class User
    {
        [Key]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public IList<Wish> Wishes { get; } = new List<Wish>();
        public IList<WishUser> AssignedTo { get; } = new List<WishUser>();
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}