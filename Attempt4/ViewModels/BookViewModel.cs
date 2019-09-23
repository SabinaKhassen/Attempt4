using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attempt4.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? Price { get; set; }
        public int GenreId { get; set; }
        public byte[] ImageData { get; set; }
    }
}