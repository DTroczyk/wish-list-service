using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class WishStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Wish> Wishes { get; } = new List<Wish>();
}
