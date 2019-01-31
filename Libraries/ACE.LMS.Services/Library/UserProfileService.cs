using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class UserProfileService
    {
        private readonly IRepository<UserProfile> _userProfileRepository;

        public UserProfileService(IRepository<UserProfile> userProfileRepository)
        {
            this._userProfileRepository = userProfileRepository;
        }

        public void UpdateTermsNConditionFlag(int profileId)
        {
            var existing = this._userProfileRepository.Table.FirstOrDefault(w => w.Id == profileId);
            if (existing != null)
            {
                existing.HasCheckedTerms = true;
                existing.TermsAgreementDate = DateTime.Now;
                this._userProfileRepository.Update(existing);
            }
        }
    }
}
