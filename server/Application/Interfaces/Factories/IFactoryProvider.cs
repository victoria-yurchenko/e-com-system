using Application.Enums;

namespace Application.Interfaces.Factories
{
    public interface IFactoryProvider
    {
        IFactory<T, TOperation>? GetFactory<T, TOperation>(FactoryType type) where T : class where TOperation : Enum;
    }
}