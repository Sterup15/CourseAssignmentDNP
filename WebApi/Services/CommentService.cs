using ApiContracts.DTOs;
using DomainEntities;
using EntitityFilters.Filters;
using Repositories;

public class CommentService
    {
        private readonly ICommentRepository commentRepo;
        private readonly IUserRepository userRepo;
        private readonly IPostRepository postRepo;

        public CommentService(ICommentRepository commentRepo, IUserRepository userRepo, IPostRepository postRepo)
        {
            this.commentRepo = commentRepo;
            this.userRepo = userRepo;
            this.postRepo = postRepo;
        }

        public async Task<CommentDto> GetSingleAsync(int commentId)
        {
            var comment = await commentRepo.GetSingleAsync(commentId); // Fetch comment
            return comment == null ? null : new CommentDto
            {
                Id = comment.Id,
                Body = comment.Body,
                Author = new UserDto
                {
                    Id = comment.UserId,
                    UserName = (await userRepo.GetSingleAsync(comment.UserId)).Username
                },
                Post = new PostDto
                {
                    Id = comment.PostId,
                    Title = (await postRepo.GetSingleAsync(comment.PostId)).Title,
                    Body = (await postRepo.GetSingleAsync(comment.PostId)).Body,
                    Author = new UserDto
                    {
                        Id = (await postRepo.GetSingleAsync(comment.PostId)).UserId,
                        UserName = (await userRepo.GetSingleAsync(comment.PostId)).Username
                    }
                }
            };
        }

        public async Task<CommentDto> CreateAsync(int postId, CreateCommentDto dto)
        {
            var comment = new Comment(dto.Body, dto.UserId, postId);
            var createdComment = await commentRepo.AddAsync(comment); // Save comment
            return new CommentDto
            {
                Id = createdComment.Id,
                Body = createdComment.Body,
                Author = new UserDto
                {
                    Id = createdComment.UserId,
                    UserName = (await userRepo.GetSingleAsync(createdComment.UserId)).Username
                },
                Post = new PostDto
                {
                    Id = createdComment.PostId,
                    Title = (await postRepo.GetSingleAsync(createdComment.PostId)).Title,
                    Body = (await postRepo.GetSingleAsync(createdComment.PostId)).Body,
                    Author = new UserDto
                    {
                        Id = (await postRepo.GetSingleAsync(createdComment.PostId)).UserId,
                        UserName = (await userRepo.GetSingleAsync(createdComment.PostId)).Username
                    }
                }
            };
        }

        public async Task<CommentDto> UpdateAsync(int commentId, UpdateCommentDto dto)
        {
            var existingComment = await commentRepo.GetSingleAsync(commentId);
            if (existingComment == null)
            {
                throw new Exception("Comment doesn't exist"); // Comment not found
            }

            existingComment.Body = dto.Body;
            Comment updatedComment = await commentRepo.UpdateAsync(existingComment); // Update comment
            return new CommentDto
            {
                Id = updatedComment.Id,
                Body = updatedComment.Body,
                Author = new UserDto
                {
                    Id = updatedComment.UserId,
                    UserName = (await userRepo.GetSingleAsync(updatedComment.UserId)).Username
                },
                Post = new PostDto
                {
                    Id = updatedComment.PostId,
                    Title = (await postRepo.GetSingleAsync(updatedComment.PostId)).Title,
                    Body = (await postRepo.GetSingleAsync(updatedComment.PostId)).Body,
                    Author = new UserDto
                    {
                        Id = (await postRepo.GetSingleAsync(updatedComment.PostId)).UserId,
                        UserName = (await userRepo.GetSingleAsync(updatedComment.PostId)).Username
                    }
                }
            };
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            var existingComment = await commentRepo.GetSingleAsync(commentId);
            if (existingComment == null)
            {
                return false; // Comment not found
            }

            await commentRepo.DeleteAsync(commentId); // Delete comment
            return true; // Success
        }

    public async Task<IEnumerable<CommentDto>> GetCommentsAsync(CommentFilter filter)
    {
        IQueryable<Comment> comments = await commentRepo.GetManyAsync(); // Get a List<Comment>

        // Apply filtering
        if (filter.UserId.HasValue)
        {
            comments = comments.Where(c => c.UserId == filter.UserId.Value);
        }

        if (!string.IsNullOrEmpty(filter.UserName))
        {
            var userId = await userRepo.GetIdByUsernameAsync(filter.UserName);
            if (userId != -1)
            {
                comments = comments.Where(c => c.UserId == userId);
            }
        }

        if (filter.PostId.HasValue)
        {
            comments = comments.Where(c => c.PostId == filter.PostId.Value);
        }

        // Project to CommentDto
        var commentDtos = new List<CommentDto>();

        foreach (var comment in comments)
        {
            var author = await userRepo.GetSingleAsync(comment.UserId); // Get user for author
            var post = await postRepo.GetSingleAsync(comment.PostId); // Get post for post
            var postAuthor = await userRepo.GetSingleAsync(post.UserId);
            
            commentDtos.Add(new CommentDto
            {
                Id = comment.Id,
                Body = comment.Body,
                Author = new UserDto
                {
                    Id = author.Id,
                    UserName = author.Username
                },
                Post = new PostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    Author = new UserDto
                    {
                        Id = postAuthor.Id,
                        UserName = postAuthor.Username
                    }
                }
                
            });
        }

        return commentDtos;
    }
}