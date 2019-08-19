using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RouteSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly ILogger _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var link = Url.Action("Index", "Home", new {id = 17,name = "abc"});
            _logger.LogInformation("合成的路由路径：" + link);
            
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}