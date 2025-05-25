namespace Store.Models
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Purchase Purchase { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
