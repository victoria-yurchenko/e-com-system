using Application.Enums;
using Application.Interfaces.Factories;

namespace Application.Factories
{
    public class StrategiesFactory<TStrategy, TOperation> : IStrategiesFactory<TStrategy, TOperation>
        where TStrategy : class
        where TOperation : Enum
    {
        private readonly IFactoryProvider _factoryProvider;

        public StrategiesFactory(IFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
        }

        public TStrategy CreateStrategy(TOperation operation)
        {
            var factory = _factoryProvider.GetFactory<TStrategy, TOperation>(FactoryType.Notification);
            if (factory == null)
            {
                throw new ArgumentException($"No factory found for {typeof(TStrategy).Name} with operation {operation}");
            }

            return factory.Create(operation);
        }
    }
}
