using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoItemMemoryProvider : IToDoItemProvider
    {
        private List<ToDoItem> _toDoItems = new List<ToDoItem>();

        public void AddToDoItem(ToDoItem newItem)
        {
            _toDoItems.Add(newItem);
        }

        public void EditToDoItemMessage(long id, string newMessage)
        {
            int i = Convert.ToInt32(id);

            _toDoItems[i].Item = newMessage;
        }

        public void ChangeStatus(long id)
        {
            int i = Convert.ToInt32(id);

            bool oldStatus = _toDoItems[i].IsCompleted;
            bool newStatus = !oldStatus;

            _toDoItems[i].IsCompleted = newStatus;
        }

        public bool DeleteDoItem(long id)
        {
            int i = Convert.ToInt32(id);
            return _toDoItems.Remove(_toDoItems[i]);
        }

        public IEnumerable<ToDoItem> ToDoItems
        {
            get
            {
                var counter = 0;
                _toDoItems.ForEach( i => i.Id = counter++);
                return _toDoItems.AsEnumerable();
            }
        }
    }
}
