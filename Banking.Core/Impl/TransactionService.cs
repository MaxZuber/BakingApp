using System;
using System.Data;
using System.Data.Entity.Core;
using Banking.Common.Identifiers;
using Banking.Common.RequestResult;
using Banking.Core.Abstract;
using Banking.DataAccess.UnitOfWork;
using Banking.Models;

namespace Banking.Core.Impl
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RequestResult<Transaction> TransferMoney(TransferMoneyViewModel transferViewModel)
        {
            var result = new RequestResult<Transaction>();
            var optimisticConcurrencyException = false;

            do
            {
                try
                {
                    _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
                    var userIssuer = _unitOfWork.UserRepository.Get(transferViewModel.IssuerId);
                    var userRecipient = _unitOfWork.UserRepository.Get(transferViewModel.RecipientId);
                    userIssuer.Balance = 200;
                    userRecipient.Balance = 300;
                    var issuerTransaction = new Transaction()
                    {
                        AccountId = transferViewModel.IssuerId,
                        Ammount = transferViewModel.Amount,
                    };

                    var recipientTransaction = new Transaction()
                    {
                        AccountId = transferViewModel.RecipientId,
                        Ammount = -transferViewModel.Amount,
                    };
                    _unitOfWork.TransactionRepository.Save(issuerTransaction);
                    _unitOfWork.TransactionRepository.Save(recipientTransaction);

                    _unitOfWork.Commit();
                    result.Status = RequestStatus.Success;
                    optimisticConcurrencyException = false;

                }
                catch (OptimisticConcurrencyException ex)
                {
                    //log error
                    optimisticConcurrencyException = true;
                }
                catch (Exception ex)
                {
                    //log exception
                    _unitOfWork.Rollback();
                    result.Status = RequestStatus.Error;
                }
            } while (optimisticConcurrencyException);

            return result;
        }

        public RequestResult<Transaction> DepositMoney(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public RequestResult<Transaction> WithdrawMoney(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
