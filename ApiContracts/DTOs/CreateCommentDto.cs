﻿namespace ApiContracts.DTOs;

public class CreateCommentDto
{
    public required string Body { get; set; }
    public required int UserId { get; set; }
}