﻿using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
    public class BookRepositoryTests : IClassFixture<BookRepositoryFixture>
    {
        private readonly LibraryManagementSystem.Repositories.BookRepository _bookRepository;

        public BookRepositoryTests(BookRepositoryFixture fixture)
        {
            _bookRepository = fixture.BookRepository;
        }

        private static Book CreateBook() => new Book("New Title", "New Author", "New Genre", "090-389-0893", DateTime.Now);

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Contains(book, books);
        }

        [Fact]
        public void GetAllBooks_WhenNoBooks_ShouldReturnEmptyList()
        {
            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Empty(books);
        }

        [Fact]
        public void GetBook_ByExistingId_ShouldReturnBook()
        {
            Book book = CreateBook();

            Book foundBook = _bookRepository.GetBookById(book.Id);

            Assert.NotNull(foundBook);
            Assert.Equal(book.Id, foundBook.Id);
        }

        [Fact]
        public void GetBook_ByUnavailableId_ShouldReturnBook()
        {
            Book foundBook = _bookRepository.GetBookById(Guid.NewGuid());

            Assert.Null(foundBook);
        }

        [Fact]
        public void AddBook_ShouldAddBookToFile()
        {
            Book book = CreateBook();

            _bookRepository.AddBook(book);
            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Contains(book, books);
        }

        [Fact]
        public void UpdateBook_ShouldUpdateBookDetails()
        {
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            string updatedTitle = "Updated Title";
            string updatedAuthor = "Updated Author";
            book.Title = updatedTitle;
            book.Author = updatedAuthor;
            Book updatedBook = _bookRepository.UpdateBook(book);

            Assert.NotEqual(book.Title, updatedBook.Title);
            Assert.NotEqual(book.Author, updatedBook.Author);
            Assert.Equal(updatedTitle, updatedBook.Title);
            Assert.Equal(updatedAuthor, updatedBook.Author);
        }

        [Fact]
        public void DeleteBook_ShouldRemoveBookFromFile()
        {
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            _bookRepository.DeleteBook(book);

            List<Book> books = _bookRepository.GetAllBooks();
            Assert.DoesNotContain(book, books);
        }

        [Fact]
        public void SaveBooks_ShouldWriteBooksToFile()
        {
            Book book = CreateBook();
            List<Book> books = new List<Book> { book };

            _bookRepository.SaveBooks(books);
            List<Book> savedBooks = _bookRepository.GetAllBooks();

            Assert.Contains(book, savedBooks);
        }
    }
}

