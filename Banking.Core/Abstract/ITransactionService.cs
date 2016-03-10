using Banking.Common.RequestResult;
using Banking.Models;

namespace Banking.Core.Abstract
{
    public interface ITransactionService
    {
        RequestResult<Transaction> TransferMoney(TransferMoneyViewModel transferViewModel);
        RequestResult<Transaction> DepositMoney(Transaction transaction);
        RequestResult<Transaction> WithdrawMoney(Transaction transaction);
    }
}
