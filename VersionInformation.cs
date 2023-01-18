using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zephry
{
    /// <summary>
    /// Static information about Assembly and Executable versions
    /// </summary>
    public static class VersionInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The formatted string containing the Executable name and full dot-sperated version</returns>
        public static string GetExecutingVersion()
        {
            return string.Format("{0} {1}.{2}.{3}.{4}", Assembly.GetEntryAssembly().GetName().Name,
                                                        Assembly.GetEntryAssembly().GetName().Version.Major,
                                                        Assembly.GetEntryAssembly().GetName().Version.Minor,
                                                        Assembly.GetEntryAssembly().GetName().Version.Build,
                                                        Assembly.GetEntryAssembly().GetName().Version.Revision);
        }
    }
}
