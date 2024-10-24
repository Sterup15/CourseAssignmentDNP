using System.Text.Json;
using ApiContracts.DTOs;
using EntitityFilters.Filters;

namespace BlazorApp.Services.CommentService;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }


    // Retrieve a single comment by its ID
    public async Task<CommentDto> GetSingleAsync(int commentId)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments/{commentId}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Create a new comment for a specific post
    public async Task<CommentDto> CreateAsync(int postId, CreateCommentDto dto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"comments/posts/{postId}/comments", dto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Update an existing comment
    public async Task<CommentDto> UpdateAsync(int commentId, UpdateCommentDto dto)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"comments/{commentId}", dto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Delete a comment by its ID
    public async Task<bool> DeleteAsync(int commentId)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"comments/{commentId}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
        return true;
    }

    // Retrieve a list of comments based on a filter
    public async Task<IEnumerable<CommentDto>> GetCommentsAsync(CommentFilter filter)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments?UserId={filter.UserId}&UserName={filter.UserName}&PostId={filter.PostId}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<CommentDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}