using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class Friend
    {
        public string? UserId1 { get; set; }
        public string? UserId2 { get; set; }
        public User? User1 { get; set; }
        public User? User2 { get; set; }
        public bool IsUser1Accept { get; set; }
        public bool IsUser2Accept { get; set; }
        public DateTime AcceptDate { get; set; }
    }
}