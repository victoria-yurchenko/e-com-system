using Application.Enums;

namespace Application.Interfaces
{
    public interface IFactoryProvider
    {
        IFactory<T>? GetFactory<T>(FactoryType type) where T : class;
    }
}