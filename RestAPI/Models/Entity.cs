using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    public abstract class Entity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
