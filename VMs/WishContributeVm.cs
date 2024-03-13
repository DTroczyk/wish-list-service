
namespace WishListApi.Models;
public class WishContributeVm
{
    public int WishContributeId { get; set; }
    public long WishId { get; set; }
    public UserVm User { get; set; } = null!;
    public decimal Contribution { get; set; }
}