namespace SeatManagementConsole
{
    public interface IAPIService<T> where T : class
    {
        List<T> GetData();
        int PostData(T data);
        void PutData(T data);
    }
}