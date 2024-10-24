using ApiContracts.DTOs;
using EntitityFilters.Filters;

namespace BlazorApp.Services.PostService;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetPostsAsync(PostFilter filter);
    Task<PostDto> GetPostByIdAsync(int postId);
    Task<PostDto> CreatePostAsync(CreateOrUpdatePostDto dto);
    Task<PostDto> UpdatePostAsync(int postId, CreateOrUpdatePostDto dto);
    Task<bool> DeletePostAsync(int postId);
    Task<IEnumerable<CommentDto>> GetCommentsForPostAsync(int postId);
}