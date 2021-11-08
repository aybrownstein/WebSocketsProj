using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketsProj.Data;

namespace WebSocketsProj.Web.Models
{
    public class SignupViewModel: User
    {
public string Password { get; set; }
    }
}
