namespace BookShop.Models
{
    using System;

    public class Purchase
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsRecalled { get; set; }
    }
}
