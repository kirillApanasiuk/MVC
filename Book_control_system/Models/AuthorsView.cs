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
        public SelectList BooksList { get; set; }
        public List<string> bookId{ get; set; }
    }

    public class AuthorForView
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Books")]
        public string books { get; set; }
    }
}
