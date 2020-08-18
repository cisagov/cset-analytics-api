using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.ViewModels.Users
{
    public class NewUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool ChangePassword { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
    }
}
