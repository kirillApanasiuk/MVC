using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_control_system.Models
{
    public class Author:AuthorBase
    {
        public List<BookAuthor> BookAuthors { get; set; }
    }

    public abstract class AuthorBase
    {
        public int Id { get; set; }
        public string Surname { get; set; }
    }
}
