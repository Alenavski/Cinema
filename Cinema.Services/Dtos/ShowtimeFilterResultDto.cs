using System;

namespace Cinema.Services.Dtos
{
    public class ShowtimeFilterResultDto
    {
        public long Id { get; set; }
        public TimeSpan Time { get; set; }
        public MovieDto Movie { get; set; }

        public ShowtimeFilterResultDto(
            long id,
            TimeSpan time,
            MovieDto movie
        )
        {
            Id = id;
            Time = time;
            Movie = movie;
        }
    }
}