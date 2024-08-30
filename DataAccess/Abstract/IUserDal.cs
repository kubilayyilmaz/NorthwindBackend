using System;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace DataAccess.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    List<UserOperationClaimDto> GetClaims(User user);
}
