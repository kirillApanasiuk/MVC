using System.Threading.Tasks;
using Book_control_system.Models;
using Book_control_system.Repositories.BooksRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Book_control_system.Controllers
{ 
    public class BooksController : Controller
    {

   
        private readonly IBookRepository _bookRepository;
       

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IActionResult> Index(int  authorId)
        {
            var booksRepresentation = new BooksView { BookList = await _bookRepository.GetBookRepresentationList(authorId),AuthorList = await _bookRepository.GetAuthorList()};
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

                await _bookRepository.AddBook(authors, bookCreating);
            } 
            return View(bookCreating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _bookRepository.GetBook((int)id);
            if (book == null) return NotFound();
            await _bookRepository.DeleteBook((int) id);
            return RedirectToAction(nameof(Index));
        }
    }
}
    