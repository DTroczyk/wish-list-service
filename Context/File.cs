using System;
using System.Collections.Generic;

namespace WishListApi.Context;

public partial class File
{
    public int FileId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();

    public virtual ICollection<Wish> Wishes { get; } = new List<Wish>();
}
