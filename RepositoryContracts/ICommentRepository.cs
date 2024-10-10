﻿using DomainEntities;

namespace Repositories;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Comment> GetSingleAsync(int id);
    Task<IQueryable<Comment>> GetManyAsync();
}