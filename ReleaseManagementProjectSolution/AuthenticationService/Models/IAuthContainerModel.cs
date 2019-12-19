using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AuthenticationService.Models
{
    public interface IAuthContainerModel
    {
        #region Members
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }
        Claim[] Claims { get; set; }
        #endregion
    }
}
