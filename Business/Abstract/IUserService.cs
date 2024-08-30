using System;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<UserOperationClaimDto>> GetClaims(User user);
    IResult Add(User user);
    IDataResult<User> GetByMail(string mail);
}
