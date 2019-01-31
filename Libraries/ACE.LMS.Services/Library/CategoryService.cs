using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services.Library
{
    public class CategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return this._categoryRepository.Table.ToList();
        }
    }
}
