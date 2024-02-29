﻿using System.ComponentModel.DataAnnotations;

namespace BasementBlog.Database.Models;

public class Post
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime PublishDate { get; set; }

    [Required]
    public DateTime ModifiedDate { get; set; }
}
