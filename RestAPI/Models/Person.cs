using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    [Table("people")]
    public class Person : Entity
    {
        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("gender")]
        public string Gender { get; set; }
    }
}
