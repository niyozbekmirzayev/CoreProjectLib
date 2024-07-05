using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public abstract class HttpBasedService<TService> : IBaseService<TService>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;

        public IConfiguration Configuration { get; }
        public ILogger<TService> Logger { get; }

        protected HttpBasedService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<TService> logger)
        {
            Configuration = configuration;
            Logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext;
        }
    }
}
