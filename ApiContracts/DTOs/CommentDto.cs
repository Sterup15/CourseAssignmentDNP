namespace ApiContracts.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public required UserDto Author { get; set; }
    public required PostDto Post { get; set; }
}