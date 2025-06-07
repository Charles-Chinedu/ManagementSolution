using ManagementSolution.Application.Responses;

namespace ManagementSolution.Client.Services.Contracts
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAll(string baseUrl);

        Task<T> GetById(string id, string baseUrl);

        Task<GeneralResponse> Insert(T item, string baseUrl);

        Task<bool> Exists(string id, string baseUrl);

        Task<GeneralResponse> Update(T item, string baseUrl);

        Task<GeneralResponse> DeleteById (string id, string baseUrl);
    }
}
