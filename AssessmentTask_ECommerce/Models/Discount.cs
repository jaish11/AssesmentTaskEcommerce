namespace AssignmentTaskECommerce.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }  // For discount amount, you could also have a percentage
        public bool IsActive { get; set; }
    }
}
