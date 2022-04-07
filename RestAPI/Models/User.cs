using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    [Table("users")]
    public class User : Entity
    {
        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("fullname")]
        public string Fullname { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
