using ManagementSolution.Application.Responses;

namespace ManagementSolution.Infrastructure.Repositories.Contracts
{
    public interface IGenericRepository<T> 
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetById(string id);

        Task<bool> Exists(string id);

        Task<GeneralResponse> Insert(T item);

        Task<GeneralResponse> Update(T item);
        
        Task<GeneralResponse> DeleteById(string id);
    }
}
