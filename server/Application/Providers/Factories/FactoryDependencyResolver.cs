using Application.Interfaces.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Providers
{
    public class FactoryDependencyResolver : IFactoryDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public FactoryDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}