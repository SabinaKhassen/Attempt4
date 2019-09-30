using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Users")]
        public virtual int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [Required]
        [Display(Name = "Books")]
        public virtual int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Books Books { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }
    }
}
