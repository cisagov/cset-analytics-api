using System;

namespace CsetAnalytics.ViewModels
{
    public class EditUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Role { get; set; }
    }
}
