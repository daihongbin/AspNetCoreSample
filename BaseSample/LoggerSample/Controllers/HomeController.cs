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
            
            return View();
        }
    }
}