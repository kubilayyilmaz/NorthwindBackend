using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt;

public class JwtHelper : ITokenHelper
{
    private IConfiguration _configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, List<UserOperationClaimDto> operationClaims)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials, user, operationClaims);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
                                                   SigningCredentials signingCredentials,
                                                   User user,
                                                   List<UserOperationClaimDto> operationClaimsDto)
    {
        var jwt = new JwtSecurityToken(issuer: _tokenOptions.Issuer,
                                       audience: _tokenOptions.Audience,
                                       claims: SetClaims(user, operationClaimsDto),
                                       notBefore: DateTime.Now,
                                       expires: _accessTokenExpiration,
                                       signingCredentials);

        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, List<UserOperationClaimDto> operationClaimsDto)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddMail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaimsDto.Select(claim => claim.Name).ToArray());

        return claims;
    }
}
