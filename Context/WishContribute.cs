using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class WishContribute
{
    public int WishContributeId { get; set; }

    public long WishId { get; set; }

    public int UserId { get; set; }

    public decimal Contribution { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Wish Wish { get; set; } = null!;
}
