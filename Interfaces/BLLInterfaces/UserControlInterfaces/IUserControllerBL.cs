using System;
using System.Collections.Generic;
using System.Text;

using Entities;

namespace Interfaces
{
    public interface IUserControllerBL : IUserAdderBL, IUserDeleterBL, IUserEditorBl, IUserDataGetterBL, IUserRewardsControllerBL
    {
    }
}
