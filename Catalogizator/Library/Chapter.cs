using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.Library
{
    class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
