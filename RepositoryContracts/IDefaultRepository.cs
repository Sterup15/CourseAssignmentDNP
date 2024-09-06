using System.Runtime.InteropServices.JavaScript;

namespace Repositories;

public interface IDefaultRepository
{
    Task<Object> AddAsync(Object objectToAdd);
    Task UpdateAsync(Object objectToUpdate);
    Task DeleteAsync(int id);
    Task<Object> GetSingleAsync(int id);
    IQueryable<Object> GetMany();
}