namespace Application.Interfaces.Factories
{
    public interface IStrategiesFactory<TStrategy, TOperation>
        where TStrategy : class
        where TOperation : Enum
    {
        TStrategy CreateStrategy(TOperation operation);
    }
}
