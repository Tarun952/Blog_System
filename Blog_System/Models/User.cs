using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Blog_System.Models;

public partial class User:IdentityUser
{
    //public int Id { get; set; }

    //public string UserName { get; set; } = null!;

    //public string Email { get; set; } =null!;

    //public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
