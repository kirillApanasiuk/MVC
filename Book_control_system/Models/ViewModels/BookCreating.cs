using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SQLitePCL;
using static Book_control_system.BusinessLogic.ValidationRules;

namespace Book_control_system.Models
{
    public class BookCreating
    {
        public int Id { get; set; }

        [StringLength(BookTitleMaxLength,MinimumLength = 2 )]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Authors { get; set; }

    }
}
