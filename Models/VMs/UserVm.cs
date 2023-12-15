namespace WishListApi.Models
{
    public class UserVm
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Wish> Wishes { get; } = new List<Wish>();
        public IList<WishUser> AssignedTo { get; } = new List<WishUser>();
    }
}