using CivilConnection.Interop.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilConnection.Interop.Services
{
    public class CommandService
    {
        public void SendCommand(DocumentWrapper document, string command)
        {
            document.SendCommand(command);
        }
    }
}
