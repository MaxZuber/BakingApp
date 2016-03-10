namespace Banking.Models
{
    public class SignupViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public void UpdateEntity(Account account)
        {
            account.Username = Username;
            account.Password = Password;
            account.Balance = 0;
        }
    }
}
