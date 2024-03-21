using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIUsuariosDataAccess.Models;

public partial class Users
{
    //[JsonIgnore]
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string? LastName { get; set; } = null;

    public string Email { get; set; } = null!;
}
