using Application.Enums;
using Application.Factories;
using Application.Interfaces.Factories;
using Domain.Entities;

namespace Application.Providers
{
    public class FactoryProvider : IFactoryProvider
    {
        private readonly IFactoryDependencyResolver _resolver;

        public FactoryProvider(IFactoryDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public IFactory<T>? GetFactory<T>(FactoryType type) where T : class
        {
            return type switch
            {
                FactoryType.Notification when typeof(T) == typeof(INotificationStrategy) => 
                    (IFactory<T>)_resolver.Resolve<NotificationFactory>(),
                _ => throw new ArgumentException($"Unknown factory type: {type}")
            };
        }
    }
}