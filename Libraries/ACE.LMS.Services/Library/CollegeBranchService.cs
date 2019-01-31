using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class CollegeBranchService
    {
        private readonly IRepository<College> _collegeRepository;
        private readonly IRepository<Branch> _branchRepository;


        public CollegeBranchService(IRepository<College> collegeRepository, IRepository<Branch> branchRepository)
        {
            this._collegeRepository = collegeRepository;
            this._branchRepository = branchRepository;
        }

        public int InsertCollege(College college)
        {
            this._collegeRepository.Insert(college);
            return college.Id;
        }

        public int InsertBranch(Branch branch)
        {
            this._branchRepository.Insert(branch);
            return branch.Id;
        }

        public List<College> GetAllColleges()
        {
            return this._collegeRepository.Table.OrderBy(o => o.Name).ToList();
        }

        public College GetDetailByCollegeId(int Id)
        {
            return this._collegeRepository.Table.ToList().FirstOrDefault(a => a.Id == Id);
        }

        public List<Branch> GetAllBranches()
        {
            return this._branchRepository.Table.OrderBy(o => o.Name).ToList();
        }
    }
}
