using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(User user)
    {
        _userDal.Add(user);

        return new SuccessResult(Messages.UserAdded);
    }

    public IDataResult<User> GetByMail(string mail)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == mail));
    }

    public IDataResult<List<UserOperationClaimDto>> GetClaims(User user)
    {
        return new SuccessDataResult<List<UserOperationClaimDto>>(_userDal.GetClaims(user));
    }
}