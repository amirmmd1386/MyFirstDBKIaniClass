namespace MyFirstDBKIaniClass.Models
{
	public class NewTodo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public string Level { get; set; }
		public string StickyNote { get; set; }
		public DateTime Time { get; set; }
		public bool Completed { get; set; }
	}
}
