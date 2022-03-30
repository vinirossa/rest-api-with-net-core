using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    [Table("books")]
    public class Book : Entity
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
