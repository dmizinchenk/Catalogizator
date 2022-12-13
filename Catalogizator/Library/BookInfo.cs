using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class BookInfo
    {
        public int Id { get; set; }
        public string? About { get; set; } = null!;
        public string? Cover { get; set; } = null!;
        public string? Publisher { get; set; } = null!;
        public int? PublishYear { get; set; }
        public int? Pages { get; set; }
        public int IdBook { get; set; }
        public Book Book { get; set; } = null!;
    }
}
