using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.Concrete;

public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
{
    public List<UserOperationClaimDto> GetClaims(User user)
    {
        using (var context = new NorthwindContext())
        {
            var result = from userClaim in context.UserOperationClaims
                         join operationClaim in context.OperationClaims
                         on userClaim.OperationClaimId equals operationClaim.Id
                         where userClaim.UserId == user.Id
                         select new UserOperationClaimDto
                         {
                             Id = operationClaim.Id,
                             Name = operationClaim.Name
                         };

            return result.ToList();
        }
    }
}