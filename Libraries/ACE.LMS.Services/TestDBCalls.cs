using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using ACE.LMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services
{
    public class TestDBCalls
    {
        public void Test()
        {
            IRepository<Student> studentRepo = new EfRepository<Student>(new LMSContext());
            List<Student> students = studentRepo.Table.ToList();
        }
    }
}
