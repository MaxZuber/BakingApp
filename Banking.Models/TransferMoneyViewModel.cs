namespace Banking.Models
{
    public class TransferMoneyViewModel
    {
        public int IssuerId { get; set; }
        public int RecipientId { get; set; }
        public decimal Amount{ get; set; }
    }
}
