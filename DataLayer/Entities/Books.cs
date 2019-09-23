namespace DataLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        public int Id { get; set; }

        public int? AuthorId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public int? Pages { get; set; }

        public int? Price { get; set; }

        [Required]
        [Display(Name = "Genres")]
        public virtual int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genres Genres { get; set; }

        public byte[] ImageData { get; set; }

        public virtual Authors Authors { get; set; }
    }
}
