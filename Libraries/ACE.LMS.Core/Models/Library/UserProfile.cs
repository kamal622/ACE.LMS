using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Models.Security;

namespace ACE.LMS.Core.Models.Library
{
    public partial class UserProfile : BaseEntity
    {
        // public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool HasCheckedTerms { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public Nullable<System.DateTime> EmailConfirmationDate { get; set; }
        public Nullable<System.DateTime> TermsAgreementDate { get; set; }
        public int BlockCount { get; set; }
        public string LastBlockedReason { get; set; }
        public string Note { get; set; }
    }
}
