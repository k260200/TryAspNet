using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IUserDeleterBL
    {
        void DeleteUser(int userId);

        void DeleteUser(User user);
    }
}
