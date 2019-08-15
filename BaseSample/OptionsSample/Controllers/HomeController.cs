using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsSample.Models;

namespace OptionsSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly MyOptions _options;

        private readonly MyOptionsWithDelegateConfig _optionsWithDelegateConfig;

        private readonly MyOptions _snapshotOptions;

        private readonly MyOptions _named_options_1;

        private readonly MyOptions _named_options_2;

        private ILogger<HomeController> _logger;

        public HomeController(IOptionsMonitor<MyOptions> optionsAccessor,IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
            IOptionsSnapshot<MyOptions> snapshotOptionsAccessor,IOptionsSnapshot<MyOptions> namedOptionsAccessor,
            ILogger<HomeController> logger)
        {
            _options = optionsAccessor.CurrentValue;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.CurrentValue;
            _snapshotOptions = snapshotOptionsAccessor.Value;
            _named_options_1 = namedOptionsAccessor.Get("named_options_1");
            _named_options_2 = namedOptionsAccessor.Get("named_options_2");
            
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            // option
            var option1 = _options.Option1;
            var option2 = _options.Option2;
            _logger.LogDebug($"options值，option1:{option1},option2:{option2}\n");

            // delegate
            var delegate_config_option1 = _optionsWithDelegateConfig.Option1;
            var delegate_config_option2 = _optionsWithDelegateConfig.Option2;
            _logger.LogDebug($"delegate:\t option1:{delegate_config_option1},option2:{delegate_config_option2}\n");
            
            // snapshotOption
            var snapshotOption1 = _snapshotOptions.Option1;
            var snapshotOption2 = _snapshotOptions.Option2;
            _logger.LogDebug($"snapshot:\t option1{snapshotOption1},option2:{snapshotOption2}\n");
            
            //namedOptions
            _logger.LogDebug("###############################################################################################");
            _logger.LogDebug($"## named_options_1:\t option1:{_named_options_1.Option1},option2:{_named_options_1.Option2} ##");
            _logger.LogDebug($"## named_options_2:\t option1:{_named_options_2.Option1},option2:{_named_options_2.Option2} ##");
            _logger.LogDebug("###############################################################################################");
            
            return View();
        }
    }
}