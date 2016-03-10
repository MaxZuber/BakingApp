using System.Collections.Generic;

namespace Banking.Models
{
    public class Account
    {
        public int Id { get ; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public byte[] RowVersion { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
