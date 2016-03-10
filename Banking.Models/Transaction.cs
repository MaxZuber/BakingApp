using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Ammount { get; set; }
        public Account Account { get; set; } 
    }
}
