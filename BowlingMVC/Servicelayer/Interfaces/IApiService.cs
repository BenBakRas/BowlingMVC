namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface IApiService
    {

        Task<List<T>> GetAsync<T>(string endpoint);
        Task<T> GetAsynced<T>(string endpoint);
        Task<T> PostAsync<T>(string endpoint, object data);


    }
}
