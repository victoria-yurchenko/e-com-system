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

        protected TEnum GetParameter<TEnum>(IDictionary<string, object> parameters, string key) where TEnum : struct, Enum
        {
            if (!parameters.TryGetValue(key, out var value) || value is not TEnum result)
            {
                _logger.LogError($"❌ Missing or invalid '{key}' parameter.");
                throw new ArgumentException($"Missing or invalid '{key}' parameter.");
            }
            return result;
        }

        public abstract Task DispatchAsync(IDictionary<string, object> parameters);
    }
}
