using Microsoft.AspNetCore.Mvc;
using MyFirstDBKIaniClass.Data;
using MyFirstDBKIaniClass.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;


namespace MyFirstDBKIaniClass.Controllers
{
	public class TodoController : Controller
	{
		private readonly TodoDbContext db;

		public TodoController(TodoDbContext db)
		{
			this.db = db;
		}
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var model = db.Todos.FirstOrDefault(t => t.Id == id);
            return View(model);
        }
        public ActionResult Index() => RedirectToAction(nameof(List));
		[HttpGet]
		public IActionResult List(int pagenumber = 0)
		{
			var skip = pagenumber * 5;
			ViewData["pagenumber"] = pagenumber;
			List<Todo> todos = db.Todos.OrderByDescending(t=> t.Id).Skip(skip).Take(5).ToList();
			ViewData["count"] = todos.Count;
			return View(todos);
		}
		[HttpPost]
		public IActionResult List(string search = "",int pageNumber = 0)
		{
            ViewData["pagenumber"] = pageNumber;
            List<Todo> todos;
				todos = db.Todos.Where(s => s.Title.Contains(search)).OrderByDescending(t => t.Id).Skip(pageNumber * 5).Take(5).ToList();
				todos = todos.Count > 0 ? todos : db.Todos.OrderByDescending(t => t.Id).Skip(pageNumber * 5).Take(5).ToList();
            ViewData["count"] = todos.Count;

            return View(todos);
		}
		
		public IActionResult Remove(int? id)
		{
			var delRow = db.Todos.FirstOrDefault(x => x.Id == id);
			db.Todos.Remove(delRow);
			db.SaveChanges();
			return RedirectToAction(nameof(List));
		}

		[HttpGet]
		public IActionResult EditTodo(int? idCom ,int? id = null)
		{
			if (id != null)
			{
				var EdRow = db.Todos.FirstOrDefault(x => x.Id == id);
				return View(EdRow);
			}
			else if (idCom != null)
			{
				var EdRow = db.Todos.FirstOrDefault(x => x.Id == idCom);
				EdRow.Completed = !(bool)EdRow.Completed;
				db.SaveChanges();

			}
			return RedirectToAction(nameof(List));
		}

		[HttpPost]
		public IActionResult EditTodo(Todo model)
		{
			var todoToUpdate = db.Todos.FirstOrDefault(x => x.Id == model.Id);
			if (todoToUpdate != null)
			{
				todoToUpdate.Description = model.Description;
				todoToUpdate.Title = model.Title;
				todoToUpdate.Author = model.Author;
				todoToUpdate.Time = model.Time;
				todoToUpdate.Level = model.Level;
				todoToUpdate.Completed = model.Completed;
				todoToUpdate.StickyNote = model.StickyNote;
				db.SaveChanges();
			}
			return RedirectToAction(nameof(List));
		}
		[HttpGet]
		public IActionResult AddEditTodo()
		{

			return View();
		}
		[HttpPost]
		public IActionResult AddEditTodo(Todo model)
		{
			db.Todos.Add(model);
			db.SaveChanges();
			return RedirectToAction(nameof(List));
		}
	}
}
