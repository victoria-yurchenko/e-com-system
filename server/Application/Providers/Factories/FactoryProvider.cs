using Application.Enums;
using Application.Factories;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;

namespace Application.Providers.Factories
{
    public class FactoryProvider : IFactoryProvider
    {
        private readonly IFactoryDependencyResolver _resolver;

        public FactoryProvider(IFactoryDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public IFactory<T, TOperation>? GetFactory<T, TOperation>(FactoryType type) where T : class where TOperation : Enum
        {

            return type switch
            {
                FactoryType.Notification when typeof(T) == typeof(INotificationStrategy) =>
                    (IFactory<T, TOperation>)_resolver.Resolve<NotificationFactory>(),
                _ => throw new ArgumentException($"Unknown factory type: {type}")
            };
        }
    }
}