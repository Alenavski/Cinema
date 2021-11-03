using System;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Api.Tools.Interfaces;
using Cinema.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Api.Tools
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private readonly IServiceProvider _services;

        public BackgroundTaskService(IServiceProvider services)
        {
            _services = services;
        }

        public void UnblockSeatWithDelay(long seatId, long ticketId, int minutesDelay)
        {
            ThreadPool.QueueUserWorkItem(
                async _ =>
                {
                    await Task.Delay(1000 * 60 * minutesDelay);

                    using var scope = _services.CreateScope();

                    var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();
                    await ticketService.DeleteSeatTicketAsync(ticketId, seatId);
                }
            );
        }

        public void DeleteEmptyTicketWithDelay(long ticketId, int minutesDelay)
        {
            ThreadPool.QueueUserWorkItem(
                async _ =>
                {
                    await Task.Delay(1000 * 60 * minutesDelay);

                    using var scope = _services.CreateScope();

                    var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();
                    await ticketService.DeleteEmptyTicketAsync(ticketId);
                }
            );
        }
    }
}