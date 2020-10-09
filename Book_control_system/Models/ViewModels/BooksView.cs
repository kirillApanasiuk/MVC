using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Book_control_system.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_control_system.Models
{
    public class BooksView
    {
        public List<BookForView> BookList;
        public List<Author> AuthorList;
        public string AuthorId { get; set; }
    }

   public  class BookForView:BookBase
    {
        public string  Authors { get; set; }
    }
}
