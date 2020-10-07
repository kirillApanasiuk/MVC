using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Data;
using Microsoft.AspNetCore.Mvc;

namespace Book_control_system.Controllers
{
    public class AuthorsController : Controller
    {

        private readonly BookControlSystemContext _context;

        public AuthorsController(BookControlSystemContext context)
        {
            _context = context;
        }
        public IActionResult Index(string bookIndex)
        {
            return View();
        }
    }
}
