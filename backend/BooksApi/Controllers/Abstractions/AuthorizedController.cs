using System;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.UseCases.Abstractions;
using BooksApi.UseCases.GenerateToken;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers.Abstractions
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class AuthorizedController: Controller
    {
        protected async Task<AbstractAnswer<string>> GenerateNewToken(IMediator mediator)
        {
            var userToken = HttpContext.User;
            var userId = userToken.Claims.First(x => x.Type == "user_id").Value;

            var newToken = await mediator.Send(new GenerateTokenRequest
            {
                UserId = Guid.Parse(userId),
            });

            var result = new AbstractAnswer<string>
            {
                Success = true,
                Data = newToken.Data,
            };
            
            return result;
        }
    }
}