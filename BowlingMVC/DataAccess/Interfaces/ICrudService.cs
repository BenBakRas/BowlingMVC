using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDTO.Interfaces

    {
        public interface ICrudService<TDto> where TDto : class
        {
            int Create(TDto dto);
            bool DeleteById(int id);
            IEnumerable<TDto> GetAll();
            TDto GetById(int id);
            bool Update(TDto dto);
        }
    }
