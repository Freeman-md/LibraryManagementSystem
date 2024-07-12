using System;
using System.Text.Json.Serialization;

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
        public Book() : this(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Now, true) { }

        // Constructor with all parameters
        [JsonConstructor]
        public Book(Guid id, string title, string author, string genre, string isbn, DateTime publishDate, bool isAvailable)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Author = author;
            Genre = genre;
            ISBN = isbn;
            PublishDate = publishDate;
            IsAvailable = isAvailable;
        }

        // Constructor without IsAvailable (defaults to true)
        public Book(string title, string author, string genre, string isbn, DateTime publishDate)
            : this(Guid.NewGuid(), title, author, genre, isbn, publishDate, true) { }

        // Constructor without PublishDate (defaults to Now)
        public Book(string title, string author, string genre, string isbn, bool isAvailable)
            : this(Guid.NewGuid(), title, author, genre, isbn, DateTime.Now, isAvailable) { }

        // Constructor with only required parameters
        public Book(string title, string author, string genre, string isbn)
            : this(Guid.NewGuid(), title, author, genre, isbn, DateTime.Now, true) { }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var book = (Book)obj;
            return Id == book.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
