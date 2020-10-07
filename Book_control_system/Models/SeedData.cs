using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Book_control_system.Data;


namespace Book_control_system.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new BookControlSystemContext(serviceProvider
                    .GetRequiredService<DbContextOptions<BookControlSystemContext>>()))
            {
                if (context.Books.Any())
                {

                   Console.WriteLine("Db has been seeded");
                    return;
                }
                Book lordOfTheRings = new Book { ReleaseDate = new DateTime(1954, 1, 20), Title = "The Lord of the Rings",BookAuthors = new List<BookAuthor>()};
                Book hobbit = new Book { ReleaseDate = new DateTime(1937, 2, 21), Title = "The Hobbit",BookAuthors = new List<BookAuthor>()};
                Book littlePrince = new Book { ReleaseDate = new DateTime(1943, 3, 23), Title = "The Little Prince",BookAuthors = new List<BookAuthor>()};

                Author tolkien = new Author {Surname = "Tolkien"};
                Author exupery = new Author {Surname = "Exupery"};

                BookAuthor lordOfTheRingsTolkien = new BookAuthor {Author = tolkien, Book = lordOfTheRings};
                BookAuthor hobbitTolkien = new BookAuthor {Author = tolkien, Book = hobbit};
                BookAuthor littlePrinceExupery = new BookAuthor {Author = exupery, Book = littlePrince};

               
                lordOfTheRings.BookAuthors.Add(lordOfTheRingsTolkien);
                hobbit.BookAuthors.Add(hobbitTolkien);
                littlePrince.BookAuthors.Add(littlePrinceExupery);

                context.Authors.AddRange(tolkien, exupery);
                context.Books.AddRange(lordOfTheRings, hobbit, littlePrince);
                context.SaveChanges();

            }
        }
    }

    
}
