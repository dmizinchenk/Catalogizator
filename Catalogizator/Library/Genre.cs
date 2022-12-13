using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Book> Books { get; set; } = new();
        //public List<Author> Authors { get; set; } = new();
    }
}
