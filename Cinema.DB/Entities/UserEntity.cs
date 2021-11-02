using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; }
        public ICollection<TicketEntity> Tickets { get; set; }
    }
}