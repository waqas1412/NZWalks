using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositeries
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? name = null, string? sortBy = null, bool isAscending = true);
        Task<Walk?> GetByIdAsync(int id);
        Task<Walk?> UpdateAsync(int id, Walk walk);
        Task<Walk?> DeleteAsync(int id);
    }
}
