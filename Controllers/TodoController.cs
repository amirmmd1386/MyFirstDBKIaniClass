using Microsoft.AspNetCore.Mvc;
using MyFirstDBKIaniClass.core;
using MyFirstDBKIaniClass.Data;
using MyFirstDBKIaniClass.Entities;
using MyFirstDBKIaniClass.Models;
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
			var model = db.Todos.ToList();
			ViewData["pagenumber"] = pagenumber;
			ViewData["count"] = model.Count;
			return View(model.ToModel(pagenumber));
		}

		[HttpPost]
		public IActionResult List(string search = "", int pageNumber = 0)
		{
			var model = db.Todos.ToList();
			ViewData["pagenumber"] = pageNumber;

			ViewData["count"] = model.Count;

			return View(model.Search(search, pageNumber));
		}

		public IActionResult Remove(int? id)
		{
			var delRow = db.Todos.FirstOrDefault(x => x.Id == id);
			db.Todos.Remove(delRow);
			db.SaveChanges();
			return RedirectToAction(nameof(List));
		}

		[HttpGet]
		public IActionResult EditTodo(int? idCom, int? id = null)
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
		public IActionResult AddEditTodo(NewTodo model)
		{
			var m = db.Todos;
			m.Add(TodoFunc.AddModel(model));
			db.SaveChanges();
			return RedirectToAction(nameof(List));
		}
	}
}
