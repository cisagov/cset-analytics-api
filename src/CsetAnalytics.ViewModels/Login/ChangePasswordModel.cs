using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CsetAnalytics.ViewModels.Login
{
    public class ChangePasswordModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }

    }
}
