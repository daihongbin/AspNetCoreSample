﻿using ConfigurationSample.Models;
 using Microsoft.AspNetCore.Mvc;
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
            var configSection2 = _config.GetSection("section2:subsection0");
            
            //绑定到实体类
            /*var starship = new Starship();
            _config.GetSection("starship").Bind(starship);*/
            var starship = _config.GetSection("starship").Get<Starship>();
            
            //绑定poco对象
            var tvShow = _config.GetSection("tvshow").Get<TvShow>();
            
            // 将数组绑定至类
            var arrayExample = _config.GetSection("array").Get<ArrayExample>();

            var jsonArrayExmaple = _config.GetSection("json_array").Get<JsonArrayExample>();
            return View();
        }
    }
}