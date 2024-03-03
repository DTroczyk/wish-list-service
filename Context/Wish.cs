using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class Wish
{
    public long WishId { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public decimal? Price { get; set; }

    public string Description { get; set; } = null!;

    public int? Image { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? Deadline { get; set; }

    public int Quantity { get; set; }

    public bool IsMaxOne { get; set; }

    public int Status { get; set; }

    public int Visibility { get; set; }

    public virtual File? ImageNavigation { get; set; }

    public virtual WishStatus StatusNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual VisibilityStatus VisibilityNavigation { get; set; } = null!;

    public virtual ICollection<WishContribute> WishContributes { get; } = new List<WishContribute>();
}
