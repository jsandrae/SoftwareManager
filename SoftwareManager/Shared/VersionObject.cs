using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SoftwareManager.Shared
{
    /// <summary>
    /// Semver format represenation of software version number
    /// </summary>
    public class VersionObject
    {
        /// <summary>
        /// Semver regex matching string including release identifiers
        /// </summary>
        //private const string CompleteValidRegexString = 
        //    @"^(0|[1-9]\d*)\." +
        //    @"(0|[1-9]\d*)\." +
        //    @"(0|[1-9]\d*)" +
        //    @"(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))" +
        //    @"?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";

        /// <summary>
        /// Simple semver regex matching string with only Major.Minor.Patch
        /// </summary>
        private const string ValidRegexString = @"^(0|[1-9]\d*)(\.(0|[1-9]\d*))?(\.(0|[1-9]\d*))?$";

        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }

        public static bool IsValidVersionString(string? stringVersion)
        {
            
            if (string.IsNullOrEmpty(stringVersion))
            {
                return false;
            }

            var m = Regex.Match(stringVersion, ValidRegexString);

            return m.Success;
        }
    }

    public class VersionCompare : Comparer<VersionObject>
    {
        public override int Compare(VersionObject x, VersionObject y)
        {
            if (x.Major.CompareTo(y.Major) != 0)
            {
                return x.Major.CompareTo(y.Major);
            }
            else if (x.Minor.CompareTo(y.Minor) != 0)
            {
                return x.Minor.CompareTo(y.Minor);
            }
            else if (x.Patch.CompareTo(y.Patch) != 0)
            {
                return x.Patch.CompareTo(y.Patch);
            }
            else
            {
                return 0;
            }

        }
    }
}
