

namespace Aegis.Services.Services.Interfaces
{
    public interface ISystemConfig
    {
        Task StartAsync(CancellationToken cancellation);

        Task StopAsync(CancellationToken cancellation);
    }
}