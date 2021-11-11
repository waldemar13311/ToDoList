using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ToDoList.Models
{
    public interface IToDoItemProvider
    {
        void AddToDoItem(ToDoItem newItem);
        void EditToDoItemMessage(long id, string newMessage);
        void ChangeStatus(long id);
        void DeleteDoItem(long id);
        IEnumerable<ToDoItem> ToDoItems { get; }
    }
}
