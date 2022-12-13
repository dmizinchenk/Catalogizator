using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string LastName { get; set; } = null!;
        public int IdBook { get; set; }
        public List<Book> Books { get; set; } = new();
        //public List<Genre> Genres { get; set; } = new();
    }
}
