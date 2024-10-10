using ApiContracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using EntitityFilters.Filters;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService postService;

        public PostsController(PostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetMany([FromQuery] PostFilter filter)
        {
            // Retrieve posts using the PostService with the filter
            var postDtos = await postService.GetPostsAsync(filter);

            // Return 404 if no posts are found
            if (postDtos == null || !postDtos.Any())
            {
                return NotFound(); // HTTP 404
            }

            // Return the list of posts with an OK (200) status code
            return Ok(postDtos); // HTTP 200
        }

        [HttpGet("{postId:int}")]
        public async Task<ActionResult<PostDto>> GetSingle(int postId)
        {
            // Retrieve a single post by ID
            var postDto = await postService.GetPostByIdAsync(postId); // This method should be implemented in your service
            
            if (postDto == null)
            {
                return NotFound(); // HTTP 404
            }

            return Ok(postDto); // HTTP 200
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreateOrUpdatePostDto dto)
        {
            // Create a new post
            var createdPost = await postService.CreatePostAsync(dto); // This method should be implemented in your service

            return CreatedAtAction(nameof(GetSingle), new { postId = createdPost.Id }, createdPost); // HTTP 201
        }

        [HttpPut("{postId:int}")]
        public async Task<ActionResult<PostDto>> Update(int postId, [FromBody] CreateOrUpdatePostDto dto)
        {
            // Update the existing post
            var updatedPost = await postService.UpdatePostAsync(postId, dto); // This method should be implemented in your service
            
            if (updatedPost == null)
            {
                return NotFound(); // HTTP 404
            }

            return Ok(updatedPost); // HTTP 200
        }

        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> Delete(int postId)
        {
            // Delete the specified post
            var result = await postService.DeletePostAsync(postId); // This method should be implemented in your service
            
            if (!result)
            {
                return NotFound(); // HTTP 404
            }

            return NoContent(); // HTTP 204
        }

        [HttpGet("{postId:int}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForPost(int postId)
        {
            // Retrieve comments for a specific post
            var comments = await postService.GetCommentsForPostAsync(postId); // This method should be implemented in your service
            
            // Return 404 if no comments are found
            if (comments == null)
            {
                return NotFound(); // HTTP 404
            }

            return Ok(comments); // HTTP 200
        }
    }
}
