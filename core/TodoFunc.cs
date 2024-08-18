using MyFirstDBKIaniClass.Entities;
using MyFirstDBKIaniClass.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstDBKIaniClass.core
{
	public static class TodoFunc
	{
		public static List<NewTodo> ToModel(this List<Todo> entities,int pagenumber)
		{
			var skip = pagenumber * 5;
			
			List<NewTodo> model = entities.OrderByDescending(s => s.Id).Skip(skip).Take(5).Select(s => new NewTodo() { Author = s.Author, Time = s.Time, Title = s.Title, Description = s.Description, Level = s.Level, Completed = s.Completed, StickyNote = s.StickyNote, Id = s.Id }).ToList();
			return model;
		}
		public static List<NewTodo> Search(this List<Todo> entities,string search, int pageNumber)
		{
			var skip = pageNumber * 5;

			List<NewTodo> todos;
			todos = entities.Where(s => s.Title.Contains(search)).OrderByDescending(t => t.Id).Skip(pageNumber * 5).Take(5).ToList().Select(n => new NewTodo()
			{
				Id = n.Id,
				Description = n.Description,
				Title = n.Title,
				Author = n.Author,
				Time = n.Time,
				Level = n.Level,
				Completed = n.Completed,
				StickyNote = n.StickyNote,
			}).ToList();
			todos = todos.Count > 0 ? todos : entities.OrderByDescending(t => t.Id).Skip(pageNumber * 5).Take(5).ToList().Select(n => new NewTodo()
			{
				Id = n.Id,
				Description = n.Description,
				Title = n.Title,
				Author = n.Author,
				Time = n.Time,
				Level = n.Level,
				Completed = n.Completed,
				StickyNote = n.StickyNote,

			}).ToList();
			return todos;
		}

		public static Todo AddModel(NewTodo model)
		{
			var models = new Todo();
			models.Description = model.Description;
			models.Title = model.Title;
			models.Author = model.Author;
			models.Time = model.Time;
			models.Level = model.Level;
			models.Completed = model.Completed;
			models.StickyNote = model.StickyNote;
			
			return models;
		}
	}
}
