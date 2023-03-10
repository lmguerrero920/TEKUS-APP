using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User usuario, IList<string> roles);



    }
}
