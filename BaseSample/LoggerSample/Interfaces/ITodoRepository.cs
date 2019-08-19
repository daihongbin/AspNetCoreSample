using LoggerSample.Models;

namespace LoggerSample.Interfaces
{
    public interface ITodoRepository
    {
        TodoItem Find(string id);
    }
}