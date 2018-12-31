using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class MemberVM
    {

        [StringLength(100)]
        public string MemberEmail { get; set; }

        [StringLength(50)]
        public string MemberPassword { get; set; }

        [Required]
        [StringLength(120)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public int? GenderTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegDate { get; set; }

        [StringLength(100)]
        public string FileImage { get; set; }

        public int? StoreId { get; set; }

        [StringLength(200)]
        public string MemberGoogleId { get; set; }

        [StringLength(200)]
        public string MemberFacebookId { get; set; }

        [StringLength(200)]
        public string AddressMemberName { get; set; }

        public int MemberTypeId { get; set; }
    }
}
