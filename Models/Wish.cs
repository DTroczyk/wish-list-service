using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class Wish
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int? Price { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime Deadline { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool IsMaxOne { get; set; }
        public IList<WishUser> AssignedTo { get; } = new List<WishUser>();

        // Wish status
        public bool IsComplete { get; set; }
        public bool IsClosed { get; set; }
    }
}