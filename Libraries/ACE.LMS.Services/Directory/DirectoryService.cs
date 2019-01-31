using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services.Directory
{
    public class DirectoryService
    {
        private readonly IRepository<City> _cityRepository;

        public DirectoryService(IRepository<City> cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        public List<City> GetAllCities()
        {
            return this._cityRepository.Table.OrderBy(o => o.Name).ToList();
        }
    }
}
