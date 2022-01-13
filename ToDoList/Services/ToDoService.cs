using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Infrastructure;
using ToDoList.Models;
using System.Web.Mvc;

namespace ToDoList.Services

{
    public class ToDoService
    {
        ToDoContext context = new ToDoContext();

        public ToDoService()
        {
        }

        public List<TodoList> getTasks(ToDoContext context)
        {
            var list = (from i in context.ToDoList orderby i.Id select i).ToList();

            List<TodoList> todoList = list;

            return list;

        }


        public void addTask(TodoList item, ToDoContext context)
        {
            context.Add(item);
            context.SaveChanges();
        }
    }
}
