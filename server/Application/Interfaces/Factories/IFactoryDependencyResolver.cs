namespace Application.Interfaces.Factories
{
    public interface IFactoryDependencyResolver
    {
        T Resolve<T>() where T : class;
    }
}