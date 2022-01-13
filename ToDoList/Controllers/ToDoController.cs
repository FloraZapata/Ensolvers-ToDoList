using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Infrastructure;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;
        readonly ToDoList.Services.ToDoService service = new Services.ToDoService();
        

        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }

        // GET /
        public ActionResult Index()
        {

            List<TodoList> todoList = new List<TodoList>();
                
            todoList=service.getTasks(context);

            return View(todoList);

        }

        // GET /todo/create
        public IActionResult Create() => View();

        // POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoList item)
        {
            
            service.addTask(item, context);
            TempData["Success"] = "The item has been added!";


            return RedirectToAction("Index");

        }

        // GET /todo/edit/5
        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        // POST /todo/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET /todo/delete/5
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
