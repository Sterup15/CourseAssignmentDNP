using ApiContracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    [HttpGet("/posts/{postId}/comments")]
    public IActionResult GetCommentsForPost(int postId)
    {
        // Logic to retrieve comments for a specific post
        return null;
    }

    [HttpGet("{id}")]
    public IActionResult GetSingle(int id)
    {
        // Logic to retrieve a single comment
        return null;
    }

    [HttpPost("/posts/{postId}/comments")]
    public IActionResult Create(int postId, [FromBody] CreateCommentDto dto)
    {
        // Logic to create a new comment for a post
        return null;
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateCommentDto dto)
    {
        // Logic to update a comment
        return null;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // Logic to delete a comment
        return null;
    }
}
