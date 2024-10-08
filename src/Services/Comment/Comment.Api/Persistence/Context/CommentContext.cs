﻿using Comment.Api.Persistence.Contracts;
using MongoDB.Driver;

namespace Comment.Api.Persistence.Context;

public class CommentContext : ICommentContext
{
    public CommentContext()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("comments_db");

        Comments = database.GetCollection<CommentEntity>("comments");
    }

    public IMongoCollection<CommentEntity> Comments { get; init; }
}