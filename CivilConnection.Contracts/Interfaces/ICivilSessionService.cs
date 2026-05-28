using CivilConnection.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivilConnection.Contracts.Interfaces
{
    public interface ICivilSessionService
    {
        CivilSessionData GetSession();
    }
}
