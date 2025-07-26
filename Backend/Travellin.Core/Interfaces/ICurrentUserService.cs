using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travellin.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        bool IsInRole(string role);
        bool IsAuthenticated { get; }
    }

}
