namespace Application.Interfaces 
{
    public interface IFactoryDependencyResolver
    {
        T Resolve<T>() where T : class;
    }
}