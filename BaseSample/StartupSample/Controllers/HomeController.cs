using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace StartupSample.Controllers
{
    public class HomeController : Controller
    {
        public IOptions<AppOptions> _appOptions;

        public HomeController(IOptions<AppOptions> appOptions)
        {
            _appOptions = appOptions;
        }

        public ActionResult Index()
        {
            ViewData["OptionsAccessor"] = _appOptions;
            return View();
        }
    }
}
