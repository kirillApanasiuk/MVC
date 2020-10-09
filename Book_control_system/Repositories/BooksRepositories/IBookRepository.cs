using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Models;

namespace Book_control_system.Repositories.BooksRepositories
{
    public interface  IBookRepository
    {
       public Task<List<BookForView>> GetBookRepresentationList(int authorIndex);
       public Task AddBook(string[] authors, BookCreating bookCreating);
       public Task DeleteBook(int id);
       public Task<Book> GetBook(int id);
       public  Task<List<Author>> GetAuthorList();
    }
}
