using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IUserEditorBl
    {
        void EditUser(int userId, User newUser);

        void EditUser(User existingUser, User newUser);
    }
}
