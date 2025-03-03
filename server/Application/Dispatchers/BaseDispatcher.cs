using Microsoft.Extensions.Logging;

namespace Application.Dispatchers
{
    public abstract class BaseDispatcher<T> where T : class
    {
        protected readonly ILogger<T> _logger;

        protected BaseDispatcher(ILogger<T> logger)
        {
            _logger = logger;
        }

        public abstract Task DispatchAsync(IDictionary<string, object> parameters);
    }
}
