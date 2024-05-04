using System;
using System.Collections.Generic;

namespace Blog_System.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
