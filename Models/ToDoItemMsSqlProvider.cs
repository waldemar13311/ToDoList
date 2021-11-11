﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ToDoList.Models
{
    public class ToDoItemMsSqlProvider : IToDoItemProvider
    {
        private UserDbContext _context;
        private string _userId;

        public void SetUserDbContext(UserDbContext ctx)
        {
            _context = ctx;
        }

        public void SetUserId(string userId)
        {
            _userId = userId;
        }

        public void AddToDoItem(ToDoItem newItem)
        {
            newItem.UserId = _userId;

            // TODO ВОТ ТУТ ОШИБКА. ЧТО С НЕЙ ДЕЛАТЬ НЕ ЗНАЮ
            // TODO СОЗДАМ КОММИТ, ПОПРОБУЮ ПЕРЕПИСАТЬ КОД
            _context.ToDoItems.Add(newItem);
            _context.SaveChanges();
        }

        public void EditToDoItemMessage(long id, string newMessage)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(long id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDoItem(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoItem> ToDoItems
        {
            get
            {
                return _context.ToDoItems;
            }
        }
    }
}
