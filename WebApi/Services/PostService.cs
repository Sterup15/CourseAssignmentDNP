using ApiContracts.DTOs;
using DomainEntities;
using EntitityFilters.Filters;
using Repositories; // Ensure this contains your repository interfaces



public class PostService
{
    private readonly IPostRepository postRepo;
    private readonly IUserRepository userRepo;
    private readonly CommentService commentService;
    
    public PostService(IPostRepository postRepo, IUserRepository userRepo, CommentService commentService)
    {
        this.postRepo = postRepo;
        this.userRepo = userRepo;
        this.commentService = commentService;
    }

    // Method to retrieve all posts or filter them
    public async Task<IEnumerable<PostDto>> GetPostsAsync(PostFilter filter)
    {
        // Assuming GetManyAsync returns a List<Post> synchronously.
        IQueryable<Post> posts = await postRepo.GetManyAsync(); // Get a List<Post>

        // Apply filters based on the filter criteria
        if (!string.IsNullOrEmpty(filter.TitleContains))
        {
            posts = posts.Where(p => p.Title.Contains(filter.TitleContains));
        }

        if (filter.UserId.HasValue)
        {
            posts = posts.Where(p => p.UserId == filter.UserId.Value);
        }

        if (!string.IsNullOrEmpty(filter.UserName))
        {
            // Assuming GetIdByUsernameAsync returns int, which is not nullable
            var userId = await userRepo.GetIdByUsernameAsync(filter.UserName);
            if (userId != -1) // Assuming -1 indicates user not found
            {
                posts = posts.Where(p => p.UserId == userId);
            }
        }

        // Project to PostDto
        var postDtos = new List<PostDto>();

        foreach (var post in posts)
        {
            var author = await userRepo.GetSingleAsync(post.UserId); // Get user for author

            postDtos.Add(new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Author = new UserDto
                {
                    Id = author.Id,
                    UserName = author.Username
                }
            });
        }

        return postDtos; // Return the list of PostDto
    }



    // Method to retrieve a single post by ID
    public async Task<PostDto> GetPostByIdAsync(int postId)
    {
        var post = await postRepo.GetSingleAsync(postId); // Assume this retrieves a post by ID
        if (post == null) return null;

        var author = await userRepo.GetSingleAsync(post.UserId);
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Author = new UserDto
            {
                Id = author.Id,
                UserName = author.Username
            }
        };
    }

    // Method to create a new post
    public async Task<PostDto> CreatePostAsync(CreateOrUpdatePostDto dto)
    {
        var post = new Post(0, dto.Title, dto.Body, dto.UserId); // Assuming a new post has ID 0
        var createdPost = await postRepo.AddAsync(post); // Add post using repository

        return new PostDto
        {
            Id = createdPost.Id,
            Title = createdPost.Title,
            Body = createdPost.Body,
            Author = new UserDto
            {
                Id = createdPost.UserId,
                UserName = (await userRepo.GetSingleAsync(createdPost.UserId)).Username // Get username
            }
        };
    }

    // Method to update an existing post
    public async Task<PostDto> UpdatePostAsync(int postId, CreateOrUpdatePostDto dto)
    {
        var post = await postRepo.GetSingleAsync(postId);
        if (post == null) return null; // If post doesn't exist, return null

        // Update post properties
        post.Title = dto.Title;
        post.Body = dto.Body;

        await postRepo.UpdateAsync(post); // Update post using repository

        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            Author = new UserDto
            {
                Id = post.UserId,
                UserName = (await userRepo.GetSingleAsync(post.UserId)).Username // Get username
            }
        };
    }

    // Method to delete a post
    public async Task<bool> DeletePostAsync(int postId)
    {
        var post = await postRepo.GetSingleAsync(postId);
        if (post == null) return false; // Return false if post doesn't exist

        await postRepo.DeleteAsync(postId); // Delete post using repository
        return true; // Return true if deletion was successful
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsForPostAsync(int postId)
    {
        return await commentService.GetCommentsAsync(new CommentFilter { PostId = postId });
    }
}
