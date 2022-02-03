using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebApi.Models;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    // Atributo utilizado el cada metodo que necesite ser
    // validado con el JWT
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            // no logeado
            context.Result = new JsonResult(
                new { 
                    message = "Unauthorized" 
                }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized 
                };
        }
    }
}