using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Book_control_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_control_system.Repositories.AuthorsRepositories
{
    public class AuthorDefaultRepository:IAuthorRepository
    {

        private readonly BookControlSystemContext _context;
        public AuthorDefaultRepository(BookControlSystemContext context)
        {
            _context = context;
        }
        public async Task<List<AuthorForView>> GetAuthorListForView(int bookId)
        {
            List<Author> authors = (bookId == 0) ? await GetAuthorList() : await GetAuthorList(bookId);
            var representAuthorList = new List<AuthorForView>();
            foreach (var author in authors)
            {
                List<Book> authorBooks = await GetAuthorBook(author.Id);
                string books = "";
                foreach (var book in authorBooks)
                {
                    if (books.Contains(book.Title))
                    {
                        continue;
                    }

                    if (books.Length != 0)
                    {
                        books += $",{book.Title}";
                        continue;
                    }

                    books += $"{book.Title}";
                }
                representAuthorList.Add(new AuthorForView()
                {
                    Id = author.Id,
                    Surname = author.Surname,
                    Books = books

                });
            }
            return representAuthorList;
        }




        private async  Task<List<Author>> GetAuthorList(int bookId)
        {
           return await _context.Authors.Where(a => a.BookAuthors.Any(ba => ba.BookId == bookId)).ToListAsync();
        }

        private async  Task<List<Author>> GetAuthorList()
        {
            return await _context.Authors.ToListAsync();
        }
        private async Task<List<Book>> GetAuthorBook(int authorId)
        {
            return await _context.Books
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId)).ToListAsync();
        }
    }
}
