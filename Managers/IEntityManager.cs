namespace SeatManagementConsole.Managers
{
    public interface IEntityManager<T> where T : class
    {
        int Add(T obj);
        List<T> Get();
    }
}