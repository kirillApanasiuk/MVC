
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_control_system.Models
{
    public class AuthorsView
    {
        public List<AuthorForView> AuthorList { get; set; }
        public List<Book> BooksList { get; set; }
        public List<string> BookId{ get; set; }
    }

    public class AuthorForView:AuthorBase
    {
        [Display(Name = "Books")]
        public string Books { get; set; }
    }
}
