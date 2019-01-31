using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Security;
using ACE.LMS.Core.Models.Library;
using ACE.LMS.Data;

namespace ACE.LMS.Services.Library
{
    public class UserService
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        public UserService(IRepository<UserProfile> userProfileRepository)
        {
            this._userProfileRepository = userProfileRepository;
        }


        public bool checkUniqueMobile(string mobileNo)
        {
            using (LMSContext entity = new LMSContext())
            {
                int cnt = entity.AspNetUsers.Where(w => w.PhoneNumber == mobileNo).Count();
                if (cnt > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<UserListData> GetUserList(string userType, int emailConfirmed, int blocked, int active, string firstName, string lastName, string email)
        {
            Nullable<bool> isEmailConfirmed = null;
            Nullable<bool> isBlocked = null;
            Nullable<bool> isActive = null;

            if (emailConfirmed > 1)
                isEmailConfirmed = emailConfirmed == 2 ? true : false;
            if (blocked > 1)
                isBlocked = blocked == 2 ? true : false;
            if (active > 1)
                isActive = active == 2 ? true : false;
            if (string.IsNullOrEmpty(firstName))
                firstName = null;
            if (string.IsNullOrEmpty(lastName))
                lastName = null;
            if (string.IsNullOrEmpty(email))
                email = null;

            using (LMSContext entity = new LMSContext())
            {
                var query = from a in entity.AspNetUsers
                            join b in entity.UserProfiles on a.UserProfile_Id equals b.Id
                            join je in entity.Students on a.Id equals je.UserId into j1
                            from e in j1.DefaultIfEmpty()
                            where a.AspNetRoles.Any(w => w.Name == (userType == "All" ? w.Name : userType))
                            && a.EmailConfirmed == (isEmailConfirmed == null ? a.EmailConfirmed : isEmailConfirmed)
                            && b.IsBlocked == (isBlocked == null ? b.IsBlocked : isBlocked)
                            && b.IsActive == (isActive == null ? b.IsActive : isActive)
                            select new UserListData
                            {
                                Id = a.Id,
                                UserProfileId = b.Id,
                                UserType = a.AspNetRoles.Select(s => s.Name).FirstOrDefault(),
                                FirstName = e == null ? "" : (e.FirstName == null ? "" : e.FirstName),
                                LastName = e == null ? "" : (e.LastName == null ? "" : e.LastName),
                                UserName = a.UserName,
                                EmailConfirmed = a.EmailConfirmed,
                                IsActive = b.IsActive,
                                IsBlocked = b.IsBlocked,
                                RegistrationDate = b.RegistrationDate
                            };
                var final = from w in query
                            where w.FirstName == (firstName == null ? w.FirstName : firstName)
                            && w.LastName == (lastName == null ? w.LastName : lastName)
                            && w.UserName.Contains((email == null ? w.UserName : email))
                            //&& w.LastName.Contains((lastName == null || lastName == "") ? (w.LastName == null ? "" : w.LastName) : lastName)
                            select w;
                //return query.ToList();
                return final.ToList();

                //return query.Where(w => w.FirstName.Contains((firstName == null || firstName == "") ? (w.FirstName == null ? "" : w.FirstName) : firstName) && w.LastName.Contains((lastName == null || lastName == "") ? (w.LastName == null ? "" : w.LastName) : lastName)).OrderByDescending(o => o.RegistrationDate).ToList();
            }
        }

        public void UpdateUserData(string userId, int userProfileId, bool isEmailConfirmed, bool isActive, bool isBlocked)
        {
            using (LMSContext entity = new LMSContext())
            {
                var existingUser = entity.AspNetUsers.FirstOrDefault(w => w.Id == userId);
                if (existingUser != null)
                {
                    existingUser.EmailConfirmed = isEmailConfirmed;
                }
                entity.SaveChanges();
            }

            var existingUserProfile = this._userProfileRepository.Table.FirstOrDefault(w => w.Id == userProfileId);
            if (existingUserProfile != null)
            {
                existingUserProfile.IsActive = isActive;
                existingUserProfile.IsBlocked = isBlocked;
                this._userProfileRepository.Update(existingUserProfile);
            }
        }

        public class UserListData
        {
            public string Id { get; set; }
            public string UserType { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public bool EmailConfirmed { get; set; }
            public int UserProfileId { get; set; }
            public bool IsActive { get; set; }
            public bool IsBlocked { get; set; }
            public DateTime RegistrationDate { get; set; }
        }
    }
}
