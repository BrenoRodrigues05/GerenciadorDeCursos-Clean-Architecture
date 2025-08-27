using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface IDTOMapper <TDto, TEntity>
    {
        TEntity ToEntity(TDto dto);
        TDto ToDTO(TEntity entity);
      

    }
}
