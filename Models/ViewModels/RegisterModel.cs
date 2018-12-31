using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [StringLength(100)]
        public string MemberEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberPassword { get; set; }

        [Required]
        [StringLength(120)]
        public string FullName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public int GenderTypeId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime RegDate { get; set; }

        [Required]
        public int MemberTypeId { get; set; }

        [Required]
        [StringLength(200)]
        public string HomeAddress { get; set; }

        [Required]
        [StringLength(10)]
        public string CodeAuth { get; set; }
    }
}
