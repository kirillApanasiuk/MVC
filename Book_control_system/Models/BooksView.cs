using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_control_system.Models
{
    public class BooksView
    {
        public List<BookForView> BookList;
        public SelectList AuthorList;
        public string authorId { get; set; }
    }

   public  class BookForView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Authors")]
        public string  authors;
    }
}
