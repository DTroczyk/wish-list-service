using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class WishUser
    {
        public string? AssignedWishId { get; set; }
        public string? AssignedUserLogin { get; set; }
        public Wish Wish { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}