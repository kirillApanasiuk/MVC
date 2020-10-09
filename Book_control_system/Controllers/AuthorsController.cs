using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Book_control_system.Models;
using Book_control_system.Repositories.AuthorsRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_control_system.Controllers
{
    public class AuthorsController : Controller
    {

        private  readonly BookControlSystemContext _context;
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(BookControlSystemContext context, IAuthorRepository authorRepository)
        {
            _context = context;
            _authorRepository = authorRepository;
        }
        public async  Task<IActionResult> Index(int bookId)
        {
            return View(new AuthorsView() { AuthorList = await _authorRepository.GetAuthorListForView(bookId), BooksList = await GetBookList() });
           /* return View();*/
        }

        private async Task<List<Book>> GetBookList()
        {
            return (await (from b in _context.Books select b).Distinct().ToListAsync());
        }

    }
}
