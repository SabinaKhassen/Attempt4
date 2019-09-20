using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attempt4.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BookId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}