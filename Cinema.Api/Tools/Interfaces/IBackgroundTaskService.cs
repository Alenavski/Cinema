namespace Cinema.Api.Tools.Interfaces
{
    public interface IBackgroundTaskService
    {
        void UnblockSeatWithDelay(long seatId, long ticketId, int minutesDelay);
        void DeleteEmptyTicketWithDelay(long ticketId, int minutesDelay);
    }
}