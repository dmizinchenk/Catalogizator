using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class BbkCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public List<Book> Books { get; set; } = new();
    }
}
