using System;
using LoggerSample.Interfaces;
using LoggerSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggerSample.Controllers
{
    public class TodoController:Controller
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger _logger;

        public TodoController(ITodoRepository todoRepository,ILogger<TodoController> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }
        
        // 当然，也可以这样子创建日志
        /*
        public TodoController(ITodoRepository todoRepository,ILoggerFactory logger)
        {
            _todoRepository = todoRepository;
            _logger = logger.CreateLogger("LoggerSample.Controllers.TodoController");
        }
        */

        public IActionResult GetById(string id)
        {
            _logger.LogInformation(LoggingEvents.GetItem,"Getting item {ID}",id);
            
            _logger.LogInformation("Getting item {ID} at {RequestTime}",id,DateTime.Now);

            var item = _todoRepository.Find(id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound,"GetById({ID}) NOT FOUND",id);
                
                // 实际生产中在catch语句块中捕捉异常，然后进行记录。，我这里为了偷懒就直接new了一个进行记录
                _logger.LogWarning(LoggingEvents.GetItemNotFound,new Exception("未找到项！"),"GetById({ID}) NOT FOUND",id);
                
                return NotFound();
            }
            
            return new ObjectResult(item);
        }
        
        // 日志作用域
        public IActionResult GetByScope(string id)
        {
            TodoItem item;
            using (_logger.BeginScope("Message attached to logs created in the using block"))
            {
                _logger.LogInformation(LoggingEvents.GetItem,"Getting item {ID}",id);
                item = _todoRepository.Find(id);
                if (item == null)
                {
                    _logger.LogWarning(LoggingEvents.GetItemNotFound,"GetById({ID}) NOT FOUND",id);
                    return NotFound();
                }
            }
            
            return new ObjectResult(item);
        }
    }
}