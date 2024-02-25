using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime RegisterDate { get; set; }

    public bool IsActive { get; set; }

    public int? AvatarImage { get; set; }

    public virtual ICollection<Friend> FriendUserId1Navigations { get; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUserId2Navigations { get; } = new List<Friend>();
}
