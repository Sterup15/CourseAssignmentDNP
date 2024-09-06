using Repositories;

namespace InMemoryRepository;

public class DefaultInMemoryRepository : IDefaultRepository
{
    public Task<object> AddAsync(object objectToAdd)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(object objectToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<object> GetMany()
    {
        throw new NotImplementedException();
    }
}