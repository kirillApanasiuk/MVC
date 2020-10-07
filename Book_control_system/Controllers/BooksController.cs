using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Book_control_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_control_system.Controllers
{ 
    public class BooksController : Controller
    {

        private readonly BookControlSystemContext _context;

        public BooksController(BookControlSystemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string authorId)
        {
            List<Book> books;
            if (!string.IsNullOrEmpty(authorId))
            {
                books = await _context.Books.Where(b => b.BookAuthors.Any(ba => ba.AuthorId == int.Parse(authorId))).ToListAsync();
            }
            else
            {
                books = await _context.Books.ToListAsync();
            }

            var representBookList = new List<BookForView>();
            foreach (var book in books)
            {
                List<Author> bookAuthors = await _context.Authors
                    .Where(a => a.BookAuthors.Any(ba => ba.BookId == book.Id)).ToListAsync();
                string authors = "";
                foreach (var author in bookAuthors)
                {
                    authors += author.Surname;
                }
                representBookList.Add(new BookForView
                {
                    Id = book.Id,Title = book.Title,ReleaseDate = book.ReleaseDate,authors = authors

                });
            }
            var booksRepresentation = new BooksView { BookList = representBookList,AuthorList = new SelectList(await (from a in _context.Authors select a.Id).Distinct().ToListAsync())};
            return View(booksRepresentation);

        }
    }
}
    