using ApiContracts.DTOs;
using EntitityFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly CommentService commentService;

    public CommentsController(CommentService commentService)
    {
        this.commentService = commentService;
    }


    [HttpGet("{commentId:int}")]
    public async Task<ActionResult<CommentDto>> GetSingle(int commentId)
    {
        var comment = await commentService.GetSingleAsync(commentId);
        if (comment == null)
        {
            return NotFound(); // Return 404 if comment not found
        }
        return Ok(comment); // Return the comment with HTTP 200
    }

    [HttpPost("posts/{postId:int}/comments")]
    public async Task<ActionResult<CommentDto>> Create(int postId, [FromBody] CreateCommentDto dto)
    {
        var createdComment = await commentService.CreateAsync(postId, dto);
        return CreatedAtAction(nameof(GetSingle), new { commentId = createdComment.Id }, createdComment); // Return 201
    }

    [HttpPut("{commentId:int}")]
    public async Task<ActionResult<CommentDto>> Update(int commentId, [FromBody] UpdateCommentDto dto)
    {
        var updatedComment = await commentService.UpdateAsync(commentId, dto);
        if (updatedComment == null)
        {
            return NotFound(); // Return 404 if comment not found
        }
        return Ok(updatedComment); 
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> Delete(int commentId)
    {
        var deleted = await commentService.DeleteAsync(commentId);
        if (!deleted)
        {
            return NotFound(); // Return 404 if comment not found
        }
        return NoContent(); // Return 204 No Content on success
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetMany([FromQuery] CommentFilter filter)
    {
        var commentDtos = await commentService.GetCommentsAsync(filter);
        
        // Return 404 if no comments are found
        if (commentDtos == null || !commentDtos.Any())
        {
            return NotFound(); // HTTP 404
        }
        return Ok(commentDtos); // Return the list of comments with HTTP 200
    }
}
