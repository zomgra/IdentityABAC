using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public double TotalPrice
        {
            get
            {
                return OrderItems.Select(X => X.Price * X.Quality).Sum();
            } }
        public double FinalPrice { 
            get 
            {
                return DiscountPersent == 0 ? TotalPrice :
                    TotalPrice - DiscountPersent;
            } }
        public double Discount { get 
            {
                return DiscountPersent != 0 ? TotalPrice * DiscountPersent / 100 : 0;
            } }
        public int DiscountPersent { get; set; }
        public string? DiscountCode { get; set; }
    }
}
