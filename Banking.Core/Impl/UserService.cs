using System;
using System.Data;
using Banking.Common.Identifiers;
using Banking.Common.RequestResult;
using Banking.Core.Abstract;
using Banking.DataAccess.UnitOfWork;
using Banking.Models;

namespace Banking.Core.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsUserAlreadyRegistered(string username)
        {
            var result = false;

            if (!string.IsNullOrEmpty(username))
            {
                var user = _unitOfWork.UserRepository.GetByUserName(username.Trim());

                if (user != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public RequestResult<Account> CreateNewUser(Account account)
        {
            var result = new RequestResult<Account>();

            if (account == null)
            {
                result.Status = RequestStatus.Error;

            } 
            else if (IsUserAlreadyRegistered(account.Username))
            {
                result.Status = RequestStatus.Error;
                result.Message = "User already exist";
            }
            else
            {
                try
                {
                    _unitOfWork.BeginTransaction(IsolationLevel.Serializable);
                    result.Obj = _unitOfWork.UserRepository.Register(account);
                    _unitOfWork.Commit();

                    result.Status = RequestStatus.Success;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    // Log exception
                    result.Status = RequestStatus.Error;
                }
            }
            _unitOfWork.Dispose();
            return result;
        } 
    }
}
