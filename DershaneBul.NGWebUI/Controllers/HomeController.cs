using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DershaneBul.NGWebUI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "CustomAuthPolicy")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await Task.Run(() => "");
            return Ok();
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
