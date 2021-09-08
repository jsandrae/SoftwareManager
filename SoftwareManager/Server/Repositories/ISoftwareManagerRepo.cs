using SoftwareManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareManager.Server.Repositories
{
    public interface ISoftwareManagerRepo
    {
        IEnumerable<Software> GetAllSoftware();
        IEnumerable<Software> GetGreaterSoftware(string version);
    }
}
