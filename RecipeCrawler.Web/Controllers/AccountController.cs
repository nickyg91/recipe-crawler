using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeCrawler.Core.Models;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }

        [HttpPost("create")]
        public async Task<Chef> CreateAccount(CreateAccountModel model)
        {
            return null;
        }
    }
}
