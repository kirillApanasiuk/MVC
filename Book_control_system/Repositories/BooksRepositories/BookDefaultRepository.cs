using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Book_control_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_control_system.Repositories.BooksRepositories
{
    public class BookDefaultRepository : IBookRepository
    {
        private readonly BookControlSystemContext _context;

        public BookDefaultRepository(BookControlSystemContext context)
        {
            _context = context;
        }

        public async Task<List<BookForView>> GetBookRepresentationList(int authorIndex)
        {
            List<Book> books = (authorIndex == 0) ? await GetBookList() : await GetBookList(authorIndex);
            var representBookList = new List<BookForView>();
            foreach (var book in books)
            {
                List<Author> bookAuthors = await GetBookAuthors(book.Id);
                string authors = "";
                foreach (var author in bookAuthors)
                {
                    if (authors.Length == 0) authors += author.Surname;
                    authors += $",{author.Surname}";
                }

                representBookList.Add(new BookForView
                {
                    Id = book.Id,
                    Title = book.Title,
                    ReleaseDate = book.ReleaseDate,
                    Authors = authors
                });
            }

            return representBookList;
        }

        public async Task AddBook(string[] authors, BookCreating bookCreating)
        {
            Book newBook = new Book
            {
                ReleaseDate = bookCreating.ReleaseDate,
                Title = bookCreating.Title,
                BookAuthors = new List<BookAuthor>()
            };
            List<Author> authorsList = new List<Author>();
            List<BookAuthor> bookAuthorsList = new List<BookAuthor>();
            foreach (var author in authors)
            {
                Author dbAuthor;
                var existedAuthor = await GetAuthor(author);
                if (existedAuthor == null)
                {
                    dbAuthor = new Author {Surname = author};
                    authorsList.Add(dbAuthor);
                }
                else
                {
                    dbAuthor = existedAuthor;
                }

                bookAuthorsList.Add(new BookAuthor {Book = newBook, Author = dbAuthor});
            }

            newBook.BookAuthors.AddRange(bookAuthorsList);
            await _context.Authors.AddRangeAsync(authorsList);
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteBook(int id)
        {
            var book = await GetBook(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBook(int index)
        {
            return await _context.Books.FindAsync(index);
        }

        private async Task<List<Book>> GetBookList()
        {
            return await _context.Books.ToListAsync();
        }

        private async Task<List<Book>> GetBookList(int authorIndex)
        {
            return await _context.Books.Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorIndex)).ToListAsync();
        }

        private async Task<List<Author>> GetBookAuthors(int bookId)
        {
            return await _context.Authors
                .Where(a => a.BookAuthors.Any(ba => ba.BookId == bookId)).ToListAsync();
        }

        private async Task<Author> GetAuthor(string author)
        {
            return await _context.Authors.Where(a => a.Surname == author).FirstOrDefaultAsync();
        }
        public  async Task<List<Author>> GetAuthorList()
        {

            return await (from a in _context.Authors select a).Distinct().ToListAsync();

        }
    }
}
