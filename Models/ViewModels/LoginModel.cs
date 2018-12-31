using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập Email")]
        public string MemberEmail { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập Password")]
        public string MemberPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
