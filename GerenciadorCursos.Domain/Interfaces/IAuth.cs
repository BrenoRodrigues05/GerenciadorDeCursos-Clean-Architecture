using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Domain.Interfaces
{
    public interface IAuth
    {
        Task<bool> Auth(string email, string password);
        Task<bool> Register(string email, string password);
        Task Logout();
    }
}
