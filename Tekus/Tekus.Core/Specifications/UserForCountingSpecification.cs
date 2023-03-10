using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.Core.Specifications
{
    public class UserForCountingSpecification : BaseSpecification<User>
    {
        public UserForCountingSpecification(UserSpecificationParams usuarioParams)
          : base(x =>
          (string.IsNullOrEmpty(usuarioParams.Search) || x.Name.Contains(usuarioParams.Search)) &&
            (string.IsNullOrEmpty(usuarioParams.Name) || x.Name.Contains(usuarioParams.Name)) &&
        (string.IsNullOrEmpty(usuarioParams.LastName) || x.LastName.Contains(usuarioParams.LastName)))
        { }


    }
}
