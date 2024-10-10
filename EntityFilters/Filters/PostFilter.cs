namespace EntitityFilters.Filters;

public class PostFilter
{
    public string TitleContains { get; set; }
    public int? UserId { get; set; }  // Nullable for optional filtering
    public string UserName { get; set; }
}
