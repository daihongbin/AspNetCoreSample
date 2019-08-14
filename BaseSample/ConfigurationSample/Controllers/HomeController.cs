﻿using Microsoft.AspNetCore.Mvc;
 using Microsoft.Extensions.Configuration;
 using Microsoft.Extensions.Logging;

 namespace ConfigurationSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly IConfiguration _config;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration config,ILogger<HomeController> logger)
        {
            _config = config;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            //获取单个数字key
            var numberConfig = _config.GetValue<int>("NumberKey", 90);
            _logger.LogInformation($"数字key为：{numberConfig}");
            
            //获取section
            var configSection1 = _config.GetSection("section1");
            var key1 = configSection1.GetValue<string>("key0");
            
            var configSection2 = _config.GetSection("section2:subsection0");
            
            return View();
        }
    }
}