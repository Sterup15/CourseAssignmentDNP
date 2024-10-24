using System.Text.Json;
using ApiContracts.DTOs;
using EntitityFilters.Filters;

namespace BlazorApp.Services.PostService;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }

    // Get a list of posts based on filters
    public async Task<IEnumerable<PostDto>> GetPostsAsync(PostFilter filter)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"posts?TitleContains={filter.TitleContains}&UserId={filter.UserId}&UserName={filter.UserName}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<PostDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Get a single post by ID
    public async Task<PostDto> GetPostByIdAsync(int postId)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"posts/{postId}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Create a new post
    public async Task<PostDto> CreatePostAsync(CreateOrUpdatePostDto dto)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("posts", dto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Update an existing post by ID
    public async Task<PostDto> UpdatePostAsync(int postId, CreateOrUpdatePostDto dto)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"posts/{postId}", dto);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    // Delete a post by ID
    public async Task<bool> DeletePostAsync(int postId)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"posts/{postId}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
        return true;
    }

    // Get comments for a specific post by ID
    public async Task<IEnumerable<CommentDto>> GetCommentsForPostAsync(int postId)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"posts/{postId}/comments");
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