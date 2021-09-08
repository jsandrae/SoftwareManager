using Serilog;
using SoftwareManager.Shared;
using System.Collections.Generic;

namespace SoftwareManager.Server.Repositories
{
    public class SoftwareManagerRepo : ISoftwareManagerRepo
    {
        /// <summary>
        /// Gets all Software
        /// </summary>
        /// <returns>Enumerable of software</returns>
        public IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1"
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
        }

        /// <summary>
        /// Gets all software greater than given version
        /// </summary>
        /// <param name="stringVersion">Query parameter for version comparision</param>
        /// <returns>Enumerable of Software with greater version numbers than given version number</returns>
        public IEnumerable<Software> GetGreaterSoftware(string stringVersion)
        {
            // Convert string version into VersionObject
            VersionObject requestedVersion = GetVersionFromString(stringVersion);

            if (requestedVersion == null)
                return null;

            // Create empty list to store software that meets criteria
            var requestedSoftwareList = new List<Software>();

            // Get list of all software
            IEnumerable<Software> fullSoftwareList = GetAllSoftware();
            VersionCompare versionCompare = new VersionCompare();

            foreach (var software in fullSoftwareList)
            {
                var softwareVersion = GetVersionFromString(software.Version);
                //if (IsSoftwareVersionGreaterThanGivenVersion(requestedVersion, software))
                //    requestedSoftwareList.Add(software);
                if (versionCompare.Compare(softwareVersion, requestedVersion) == 1)
                    requestedSoftwareList.Add(software);
            }

            return requestedSoftwareList;
        }

        /// <summary>
        /// Converts string Version into VersionObject to allow for comparison 
        /// </summary>
        /// <param name="stringVersion">String representation of version</param>
        /// <returns>Valid semver VersionObject representation of string or null</returns>
        private VersionObject GetVersionFromString(string? stringVersion)
        {
            if (!VersionObject.IsValidVersionString(stringVersion))
            {
                Log.Error($"Error SoftwareManagerRepo.GetVersionFromString: invalid version given {stringVersion}.");
                return null;
            }

            // Split string on delimiter: .
            var subversions = stringVersion.Split('.');            

            // Create an array to hold version integers 
            int[] versionInt = {0, 0, 0};
            
            for (var i = 0; i < subversions.Length; i++)
            {
                var couldParse = int.TryParse(subversions[i], out var intResult);
                if (!couldParse)
                {
                    var message =
                        $"Error SoftwareManagerRepo.GetVersionFromString: Could not get version from {stringVersion}.";
                    Log.Error(message);
                    return null;
                }

                versionInt[i] = intResult;
            }
            
            var major = versionInt[0];
            var minor = versionInt[1];
            var patch = versionInt[2];

            return new VersionObject { Major = major, Minor = minor, Patch = patch };
        }

        /// <summary>
        /// Alternative comparison method no longer used but kept for testing.
        /// </summary>
        /// <param name="requestedVersion">Standalone version for comparison</param>
        /// <param name="software">Software to check version comparison</param>
        /// <returns>True if Software version is greater than requested version, false otherwise</returns>
        private bool IsSoftwareVersionGreaterThanGivenVersion(VersionObject requestedVersion, Software software)
        {
            var softwareVersion = GetVersionFromString(software.Version);
            
            if (softwareVersion.Major > requestedVersion.Major)
                return true;
            else if (softwareVersion.Major < requestedVersion.Major)
                return false;

            // If equal, compare minor version number
            if (softwareVersion.Minor > requestedVersion.Minor)
                return true;
            else if (softwareVersion.Minor < requestedVersion.Minor)
                return false;

            if (softwareVersion.Patch > requestedVersion.Patch)
                return true;
            else
                return false;
        }
    }
}
