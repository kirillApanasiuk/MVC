﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_control_system.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string  Surname {get;set;}


        public List<BookAuthor> BookAuthors { get; set; }
    }
}
