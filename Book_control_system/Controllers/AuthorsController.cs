using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Book_control_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_control_system.Controllers
{

    public class AuthorsController : Controller
    {

        private  readonly BookControlSystemContext _context;

        public AuthorsController(BookControlSystemContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index(string bookId)
        {

            List<Author> authors;
            if (!string.IsNullOrEmpty(bookId))
            {
                authors = await _context.Authors.Where(a => a.BookAuthors.Any(ba => ba.BookId == int.Parse(bookId))).ToListAsync();
            }
            else
            {
                authors = await _context.Authors.ToListAsync();
            }

            var representAuthorList = new List<AuthorForView>();

            foreach (var author in authors)
            {
                List<Book> authorBooks = await _context.Books
                    .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == author.Id)).ToListAsync();
                string books = "";
                foreach (var book in authorBooks)
                {
                    if (books.Contains(book.Title))
                    {
                        continue;   
                    }
                    if (books.Length != 0)
                    {
                        books +=$",{book.Title}";
                        continue;
                    }

                    books +=$"{book.Title}";
                }

               
                representAuthorList.Add(new AuthorForView()
                {
                   Id = author.Id,
                   Surname = author.Surname,
                   books = books

                });
            }
            return View( new AuthorsView(){AuthorList = representAuthorList,BooksList = new SelectList(await (from b in _context.Books select b.Id).Distinct().ToListAsync())});
        }
    }
}
