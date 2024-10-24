using ApiContracts.DTOs;
using EntitityFilters.Filters;

namespace BlazorApp.Services.CommentService;

public interface ICommentService
{
    Task<CommentDto> GetSingleAsync(int commentId);
    Task<CommentDto> CreateAsync(int postId, CreateCommentDto dto);
    Task<CommentDto> UpdateAsync(int commentId, UpdateCommentDto dto);
    Task<bool> DeleteAsync(int commentId);
    Task<IEnumerable<CommentDto>> GetCommentsAsync(CommentFilter filter);
}