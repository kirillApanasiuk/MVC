﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_control_system.Models
{
    public class AuthorsView
    {
        public List<Author> Authors { get; set; }
        public List<string> bookList { get; set; }
    }
}
