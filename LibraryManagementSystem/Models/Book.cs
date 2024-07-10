using System;
namespace LibraryManagementSystem.Models
{
	public class Book
	{
		public int Id { get; private set; }
		public string Author { get; set; }
		public string Genre { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsAvailable { get; set; }


        public Book(int id, string author, string genre, string isbn, DateTime publishDate, bool isAvailable)
		{
			Id = id;
			Author = author;
			Genre = genre;
			ISBN = isbn;
			PublishDate = publishDate;
			IsAvailable = isAvailable;
		}
	}
}

