using System.Collections.Generic;
using ToDoApiForMobile.Models;

namespace ToDoApiForMobile.Interfaces
{
    public interface IToDoRepository
    {
        bool DoesItemExist(string id);

        IEnumerable<ToDoItem> All { get; }

        ToDoItem Find(string id);

        void Insert(ToDoItem item);

        void Update(ToDoItem item);

        void Delete(string id);
    }
}
