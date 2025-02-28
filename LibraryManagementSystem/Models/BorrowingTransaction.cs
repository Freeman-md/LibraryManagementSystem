﻿using System.Net.WebSockets;
using System.Text.Json.Serialization;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransaction : Transaction
{
    
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }
    public double Fine { get; set; }
    public bool IsReturned {
        get {
            return ReturnDate.HasValue;
        }
    }

    public const int DEFAULT_BORROWING_DURATION_IN_DAYS = 7;
    public const double FINE_RATE_PER_DAY = 10.0;
    public const int GRACE_PERIOD_IN_DAYS = 3;
    public const double MAX_FINE = 200.0;
    public const int MAX_OVERDUE_DAYS = 14;

    [JsonConstructor]
    public BorrowingTransaction(Guid id, Book book, Member member, DateTime dueDate, double fine = 0.0, DateTime? returnDate = null)
        : base(id, book, member)
    {
        DueDate = dueDate;
        ReturnDate = returnDate;
        Fine = fine;
    }

    public BorrowingTransaction(Book book, Member member, DateTime dueDate, double fine = 0.0, DateTime? returnDate = null)
        : base(book, member)
    {
        DueDate = dueDate;
        ReturnDate = returnDate;
        Fine = fine;
    }

    public override void PrintDetails()
        {
            base.PrintDetails();
            ConsoleHelper.PrintColoredText("Due Date: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{DueDate.ToShortDateString()}, ", ConsoleColor.White);

            if (ReturnDate.HasValue)
            {
                ConsoleHelper.PrintColoredText("Return Date: ", ConsoleColor.Green);
                ConsoleHelper.PrintColoredText($"{ReturnDate.Value.ToShortDateString()}, ", ConsoleColor.White);
            }

            ConsoleHelper.PrintColoredText("Fine: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Fine:C}, ", ConsoleColor.White);

            ConsoleHelper.PrintColoredText("Is Returned: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{IsReturned}", ConsoleColor.White);

            Console.WriteLine();
        }
}
