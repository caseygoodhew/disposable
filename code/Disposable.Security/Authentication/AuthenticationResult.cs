using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationResult
    {
        public AuthenticationResult(AuthenticationStatus status)
        {
            Status = status;
        }
        
        public AuthenticationStatus Status { get; private set; }
    }
}
