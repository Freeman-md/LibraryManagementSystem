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

        // Constructor with all parameters
        [JsonConstructor]
        public Book(Guid id, string title, string author, string genre, string isbn, DateTime publishDate = default(DateTime), bool isAvailable = true)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Author = author;
            Genre = genre;
            ISBN = isbn;
            PublishDate = publishDate == default(DateTime) ? DateTime.Now : publishDate;
            IsAvailable = isAvailable;
        }

        public Book(string title, string author, string genre, string isbn, DateTime publishDate = default(DateTime), bool isAvailable = true)
            : this(Guid.NewGuid(), title, author, genre, isbn, publishDate, isAvailable)
        {
        }

        public override bool Equals(object? obj)
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
