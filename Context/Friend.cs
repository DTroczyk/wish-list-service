using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class Friend
{
    public int FriendId { get; set; }

    public int UserId1 { get; set; }

    public int UserId2 { get; set; }

    public DateTime? AcceptDate { get; set; }

    public bool? IsConfirmed { get; set; }

    public virtual User UserId1Navigation { get; set; } = null!;

    public virtual User UserId2Navigation { get; set; } = null!;
}
