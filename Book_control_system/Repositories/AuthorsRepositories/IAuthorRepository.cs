using System.Collections.Generic;
using System.Threading.Tasks;
using Book_control_system.Models;

namespace Book_control_system.Repositories.AuthorsRepositories
{
    public interface IAuthorRepository
    {
        public Task<List<AuthorForView>> GetAuthorListForView(int bookId);
    }
}
