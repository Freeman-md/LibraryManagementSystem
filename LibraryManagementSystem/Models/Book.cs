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

        public void MarkAsBorrowed()
        {
            this.IsAvailable = false;
        }

        public void MarkAsReturned()
        {
            this.IsAvailable = true;
        }

        public void PrintBookDetails()
        {
            ConsoleHelper.PrintColoredText("Title: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Title}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Author: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Author}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Genre: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Genre}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("ISBN: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{ISBN}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Published: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{PublishDate.ToShortDateString()}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Available: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{IsAvailable}", ConsoleColor.White);
            Console.WriteLine();
        }
    }
}
