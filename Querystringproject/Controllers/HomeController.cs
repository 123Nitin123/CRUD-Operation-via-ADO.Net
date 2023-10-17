using Microsoft.AspNetCore.Mvc;
using Querystringproject.Models;
using System.Diagnostics;



namespace Querystringproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login ad)
        {
            LoginDb dbop = new LoginDb();
            int res = dbop.LoginCheck(ad);
            if (res == 1)
            {
              return  RedirectToAction("Index", "Record");
            }
            else
            {
                TempData["msg"] = "Admin id or Password is wrong.!";
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}