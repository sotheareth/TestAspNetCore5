namespace TestAspNetCore_Core.Entities
{
    public class Order : BaseEntity
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}