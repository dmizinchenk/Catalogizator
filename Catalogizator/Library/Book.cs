using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public long Isbn { get; set; }
        public int ReleaseYear { get; set; }
        public int IdBbkCode { get; set; }
        public BbkCode BbkCode { get; set; } = null!;
        public BookInfo? Info { get; set; }
        public int IdAuthor { get; set; }
        public Author Author { get; set; } = null!;
        public List<Genre> Genres { get; set; } = new();
    }
}
