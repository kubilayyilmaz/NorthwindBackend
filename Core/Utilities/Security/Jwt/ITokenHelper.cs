using System;
using Core.Entities.Concrete;
using Core.Entities.Dtos;

namespace Core.Utilities.Security.Jwt;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, List<UserOperationClaimDto> operationClaims);
    
}
