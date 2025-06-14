namespace Application.Interfaces.Factories
{
    public interface IFactory<T, TOperation> where T : class where TOperation : Enum
    {
        T Create(TOperation operation);
    }
}