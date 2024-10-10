namespace ApiContracts.DTOs;

public class PostDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required UserDto Author { get; set; }
}