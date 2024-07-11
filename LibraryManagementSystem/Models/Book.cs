using System;
namespace LibraryManagementSystem.Models
{
	public class Book
	{
        public Guid Id { get; private set; }
        public string Title { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsAvailable { get; set; }

        // Default constructor
        public Book() : this(null, null, null, null, DateTime.Now, true) { }

        // Constructor with all parameters
        public Book(string title, string author, string genre, string isbn, DateTime publishDate, bool isAvailable)
        {
            Id = Guid.NewGuid();
            Author = author;
            Genre = genre;
            ISBN = isbn;
            PublishDate = publishDate;
            IsAvailable = isAvailable;
        }

        // Constructor without IsAvailable (defaults to true)
        public Book(string title, string author, string genre, string isbn, DateTime publishDate)
            : this(title, author, genre, isbn, publishDate, true) { }

        // Constructor with only required parameters
        public Book(string title, string author, string genre, string isbn)
            : this(title, author, genre, isbn, DateTime.Now, true) { }
    }
}

