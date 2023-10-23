using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Shared.Helpers;
using POS.Shared.ViewModels;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LoginController :Controller
    {
        [HttpGet("Login/")]
        public ApiReturn<AuthReturn> Login()
        {
            ApiReturn<AuthReturn> apiReturn = new ApiReturn<AuthReturn>();
            try
            {
                apiReturn.Data = new AuthReturn();
                apiReturn.Data.EmpNum = User.Claims.FirstOrDefault(x => x.Type == IdentityHelper.Claims.EmpNumber).Value;
                apiReturn.Data.Name = User.Claims.FirstOrDefault(x => x.Type == IdentityHelper.Claims.Name).Value;
            }
            catch (Exception e)
            {
                apiReturn.Data = null;
                apiReturn.AddError("Oops, Something Went Wrong");
            }
            return apiReturn;
        }

    }
}