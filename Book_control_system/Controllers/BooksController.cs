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
                    if (authors.Length == 0) authors += author.Surname;
                    authors += $",{author.Surname}";
                }
                representBookList.Add(new BookForView
                {
                    Id = book.Id,Title = book.Title,ReleaseDate = book.ReleaseDate,authors = authors

                });
            }
            var booksRepresentation = new BooksView { BookList = representBookList,AuthorList = new SelectList(await (from a in _context.Authors select a.Id).Distinct().ToListAsync())};
            return View(booksRepresentation);

        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Authors")]
            BookCreating bookCreating)
        {
            if (ModelState.IsValid)
            {
                string[] authors = bookCreating.Authors.Split(',');
                foreach (var author in authors)
                {
                    if (author.Length > BusinessLogic.ValidationRules.AuthorSurnameMaxLength)
                    {
                        ViewData["Error"] = "The each author surname must be less than 10 symbols";
                        return View();
                    }
                }
                Book newBook = new Book { ReleaseDate = bookCreating.
                    ReleaseDate, Title =bookCreating.Title, BookAuthors = new List<BookAuthor>() };
                List<Author> authorsList = new List<Author>();
                List<BookAuthor> bookAuthorsList = new List<BookAuthor>();
                foreach (var author in authors)
                {
                    Author dbAuthor = null;
                    var existedAuthor  = await  _context.Authors.Where(a => a.Surname == author).FirstOrDefaultAsync();
                    if (existedAuthor == null)
                    {
                        dbAuthor = new Author { Surname = author };
                        authorsList.Add(dbAuthor);
                    }
                    else
                    {
                        dbAuthor = existedAuthor;
                    }
                    bookAuthorsList.Add(new BookAuthor { Book = newBook, Author = dbAuthor });
                }
                newBook.BookAuthors.AddRange(bookAuthorsList);
                await _context.Authors.AddRangeAsync(authorsList);
                await _context.Books.AddAsync(newBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            return View(bookCreating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
    