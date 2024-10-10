﻿namespace DomainEntities;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Comment(int id, string body, int userId, int postId)
    {
        Id = id;
        Body = body;
        UserId = userId;
        PostId = postId;
    }

    public Comment(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}