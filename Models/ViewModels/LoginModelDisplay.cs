using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LoginModelDisplay
    {
        public int MemberAccountId { get; set; }
        public string MemberEmail { get; set; }
        public string MemberName { get; set; }
        public bool RememberMe { get; set; }
        public int MemberTypeId { get; set; }
    }
}
