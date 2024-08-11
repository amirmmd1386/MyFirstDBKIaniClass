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
		public ActionResult Index() => RedirectToAction(nameof(List));

		public IActionResult List()
		{
			List<Todo> todos = db.Todos.ToList();
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
		public IActionResult EditTodo(int? id = null)
		{
			if (id != null)
			{
				var EdRow = db.Todos.FirstOrDefault(x => x.Id == id);

				return View(EdRow);
			}
		
		
			return View();
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
