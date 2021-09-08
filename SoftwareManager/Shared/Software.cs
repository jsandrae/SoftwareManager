using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManager.Shared
{
    public class Software
    {
        /// <summary>
        /// Name of software
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// String representation of Semver version
        /// TODO: Consider changing this from string to VersionObject once database is connected.
        /// </summary>
        public string Version { get; set; }
    }
}
