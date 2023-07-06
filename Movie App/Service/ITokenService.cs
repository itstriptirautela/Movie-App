using System;

using Movie_App.Model;

namespace Movie_App.Service
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}


