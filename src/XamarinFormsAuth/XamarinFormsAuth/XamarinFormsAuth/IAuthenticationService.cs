using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsAuth
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync();
    }
}
