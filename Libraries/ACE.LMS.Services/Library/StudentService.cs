using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class StudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public int Insert(Student student)
        {
            this._studentRepository.Insert(student);
            return student.Id;
        }

        public void Update(Student student)
        {
            //Student existing = this._studentRepository.GetById(student.Id);
            //student.CreatedBy = existing.CreatedBy;
            //student.CreatedOn = existing.CreatedOn;
            this._studentRepository.Update(student);
        }

        public Student StudentSearch(string searchField, string searchString)
        {
            if (searchField == "Email")
            {
                return this._studentRepository.Table.Where(
                   w => w.Email==searchString).FirstOrDefault();
            }
            else if (searchField == "Mobile")
            {
                return this._studentRepository.Table.Where(
                  w => w.Mobile == searchString).FirstOrDefault();
            }
            else if (searchField == "EnrollmentNo")
            {
                return this._studentRepository.Table.Where(
                  w => w.EnrollmentNo == searchString).FirstOrDefault();
            }
            else
                return null;
        }

        public IQueryable<Student> GetStudentNames(string searchString)
        {
            return
                this._studentRepository.Table.Where(
                    w => w.FirstName.Contains(searchString) || w.LastName.Contains(searchString));
        }

        public Student GetStudentDetailById(int Id)
        {
            return this._studentRepository.Table.FirstOrDefault(a => a.Id == Id);
        }

        public Student GetStudentByUserId(string userId)
        {
            return this._studentRepository.Table.FirstOrDefault(a => a.UserId == userId);
        }

        public List<Student> GetAllStudents(string enrollmentNo)
        {
            return this._studentRepository.Table.Where(w => w.EnrollmentNo.Contains((enrollmentNo == null || enrollmentNo == "") ? w.EnrollmentNo : enrollmentNo)).OrderBy(o => o.CreatedBy).ToList();
        }
    }
}
