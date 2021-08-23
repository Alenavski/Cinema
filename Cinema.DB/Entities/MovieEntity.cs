using System;
using System.Collections.Generic;

namespace Cinema.DB.Entities
{
    public class MovieEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] Poster { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
    }
}