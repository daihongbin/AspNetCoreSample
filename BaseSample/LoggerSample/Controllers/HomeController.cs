using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggerSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("这是一条info级别的日志");

            var p1 = "parm1";
            var p2 = "parm2";
            _logger.LogInformation("Parameter values: {p2}, {p1}",p1,p2);
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}