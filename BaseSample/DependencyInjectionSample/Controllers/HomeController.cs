using DependencyInjectionSample.Models;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyDependency _myDependency;

        private readonly ILogger<HomeController> _logger;

        public OperationService OperationService { get; }

        public IOperationTransient TransientOperation { get; }

        public IOperationScoped ScopedOperation { get; }

        public IOperationSingleton SingletonOperation { get; }

        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public HomeController(IMyDependency myDependency,ILogger<HomeController> logger,OperationService operationService,IOperationTransient transientOperation,IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,IOperationSingletonInstance singletonInstanceOperation)
        {
            _myDependency = myDependency;
            _logger = logger;

            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("\n");
            _logger.LogInformation("===============================================================================================");
            _logger.LogInformation($"{nameof(TransientOperation)} Guid:{TransientOperation.OperationId}");
            _logger.LogInformation($"{nameof(ScopedOperation)} Guid:{ScopedOperation.OperationId}");
            _logger.LogInformation($"{nameof(SingletonOperation)} Guid:{SingletonOperation.OperationId}");
            _logger.LogInformation($"{nameof(SingletonInstanceOperation)} Guid:{SingletonInstanceOperation.OperationId}");
            _logger.LogInformation("===============================================================================================");
            _logger.LogInformation("\n");

            await _myDependency.WriteMessage("调用信息");
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
