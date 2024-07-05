using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreProjectLib.Services
{
    public interface IBaseService<TService>
    {
        IConfiguration Configuration { get; }
        ILogger<TService> Logger { get; }
    }
}
