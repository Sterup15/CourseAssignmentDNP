namespace EntitityFilters.Filters;

public class CommentFilter
{
    public int? UserId { get; set; } // Nullable to allow filtering by user ID
    public string? UserName { get; set; } // Nullable for username filtering
    public int? PostId { get; set; } // Nullable to allow filtering by post ID
}
