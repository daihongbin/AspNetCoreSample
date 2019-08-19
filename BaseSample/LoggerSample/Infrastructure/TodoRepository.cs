using System.Collections.Generic;
using System.Linq;
using LoggerSample.Interfaces;
using LoggerSample.Models;

namespace LoggerSample.Infrastructure
{
    public class TodoRepository:ITodoRepository
    {
        private List<TodoItem> _todoItems = new List<TodoItem>
        {
            new TodoItem
            {
                Key = "1",
                Name = "One",
                IsComplete = true
            },
            new TodoItem
            {
                Key = "2",
                Name = "Two",
                IsComplete = false
            },
            new TodoItem
            {
                Key = "3",
                Name = "Three",
                IsComplete = true
            },
            new TodoItem
            {
                Key = "4",
                Name = "Four",
                IsComplete = false
            }
        };
        
        public TodoItem Find(string id)
        {
            return _todoItems.FirstOrDefault(f => f.Key == id);
        }
    }
}