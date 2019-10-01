using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attempt4.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        public int? AuthorId { get; set; }

        [RegularExpression(@"[A-Za-z]@")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки не менее 3х символов и не более 50")]
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? Price { get; set; }
        public int GenreId { get; set; }
        public byte[] ImageData { get; set; }
    }
}