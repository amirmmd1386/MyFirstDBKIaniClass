using Microsoft.EntityFrameworkCore;
using MyFirstDBKIaniClass.Entities;

namespace MyFirstDBKIaniClass.Data
{
	public class TodoDbContext : DbContext
	{
		public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
		{

		}
		public DbSet<Todo> Todos { get; set; }

	}
}
