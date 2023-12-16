namespace WishListApi.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public string? FieldName { get; set; }
        public string Code { get; set; }
    }
}