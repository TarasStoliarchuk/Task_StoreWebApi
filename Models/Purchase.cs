namespace Store.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string PurchaseNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
    }
}
