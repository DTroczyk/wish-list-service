namespace WishListApi.Models;
public class WishVm
{
    public long WishId { get; set; }
    public UserVm User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int? Image { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime Deadline { get; set; }
    public int Quantity { get; set; }
    public bool IsMaxOne { get; set; }
    public string Status { get; set; }
    public ICollection<WishContributeVm> WishContributes { get; set; }
    public decimal FillPercent { get; set; }
}