using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_control_system.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string  Title {get;set;}
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }

    }
}
